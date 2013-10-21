using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using EasyAccess.Infrastructure.Util.Encryption;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Configurations;

namespace EasyAccess.Authorization
{
    public class AuthorizationManager
    {
        private static AuthorizationManager _instance = null;
        private static readonly object Locker = new object(); 
        private readonly ReaderWriterLockSlim _rwLocker = new ReaderWriterLockSlim();

        private readonly List<Menu> _menuLst = new List<Menu>();
        private readonly List<Permission> _permissionLst = new List<Permission>();

        private readonly Dictionary<string, List<MenuDto>> _tokenToMenuItem = new Dictionary<string, List<MenuDto>>();
        private readonly Dictionary<string, string[]> _tokenToPermission = new Dictionary<string, string[]>();
        private readonly Dictionary<string, long[]> _tokenToRoleId = new Dictionary<string, long[]>();

        private static readonly string[] TokenDivider = new string[] { "^%y7@&#l,58", "%fa)ft'rtq2", "2a!4%}qwr]" };

        private AuthorizationManager()
        {
            var ctx = new EasyAccessContext();
            _menuLst = ctx.Menus.ToList();
            _permissionLst = ctx.Permissions.ToList();
        }

        /// <summary>
        /// 获取AuthorizationManager的唯一实例
        /// </summary>
        /// <returns></returns>
        public static AuthorizationManager GetInstance()
        {
            if (_instance == null)
            {
                lock (Locker)
                {
                    if (_instance == null)
                    {
// ReSharper disable PossibleMultipleWriteAccessInDoubleCheckLocking
                        return _instance = new AuthorizationManager();
// ReSharper restore PossibleMultipleWriteAccessInDoubleCheckLocking
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="roleList"></param>
        /// <param name="permissionList"></param>
        /// <returns></returns>
        public string GetToken(ICollection<Role> roleList, ICollection<Permission> permissionList)
        {
            string token = string.Empty;
            var roleIdLst = roleList.OrderBy(x => x.Id).Select(x => x.Id).ToArray();
            var dividerCount = TokenDivider.Length;
            for (int i = 0; i < roleIdLst.Count(); i++)
            {
                token += TokenDivider[i % dividerCount] + roleIdLst[i];
            }
            token = MD5Encryption.Encrypt(token);

            _rwLocker.EnterUpgradeableReadLock();
            try
            {
                if (!_tokenToRoleId.ContainsKey(token))
                {
                    _rwLocker.EnterWriteLock();
                    try
                    {
                        _tokenToRoleId.Add(token, roleIdLst);
                        if (permissionList != null)
                        {
                            _tokenToPermission.Add(token, permissionList.Select(x => x.Id).ToArray());
                        }
                    }
                    finally
                    {
                        _rwLocker.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _rwLocker.ExitUpgradeableReadLock();
            }
           

            return token;
        }

        /// <summary>
        /// 获取角色Id列表
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>角色Id列表</returns>
        private long[] GetRoleIdList(string token)
        {
            long[] roldIdLst;
            _rwLocker.EnterReadLock();
            try
            {
                if (!_tokenToRoleId.TryGetValue(token, out roldIdLst))
                {
                    throw new IdentityNotMappedException("未能根据用户自定义标识关联到相应角色Id");
                }
            }
            finally
            {
                _rwLocker.ExitReadLock();
            }
            return roldIdLst;
        }

        /// <summary>
        /// 判断字典中是否存在Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsExistToken(string token)
        {
            return _tokenToRoleId.ContainsKey(token);
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>用户菜单项</returns>
        public List<MenuDto> GetMenu(string token)
        {
            List<MenuDto> returnVal = null;
            _rwLocker.EnterUpgradeableReadLock();
            try
            {
                if (!_tokenToMenuItem.TryGetValue(token, out returnVal))
                {
                    returnVal = BuildMenu(token);
                    _rwLocker.EnterWriteLock();
                    try
                    {
                        _tokenToMenuItem.Add(token, returnVal);
                    }
                    finally
                    {
                        _rwLocker.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _rwLocker.ExitUpgradeableReadLock();
            }
            return returnVal;
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="token">用户角色标识</param>
        /// <returns>菜单项</returns>
        public List<MenuDto> GetSubMenu(string menuId, string token)
        {
            List<MenuDto> returnVal = null;
            _rwLocker.EnterReadLock();
            try
            {
                returnVal = _tokenToMenuItem.First(e => e.Key == token).Value
                    .Where(e => e.ParentId == menuId).Select(e => e).ToList<MenuDto>();

            }
            finally
            {
                _rwLocker.ExitReadLock();
            }
            return returnVal;
        }

        /// <summary>
        /// 建立菜单字典并返回用户菜单项
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>用户菜单项</returns>
        private List<MenuDto> BuildMenu(string token)
        {
            var menuAccumulator = new Dictionary<string, MenuDto>();

            var permissions = GetPermissions(token);

            foreach (var permission in permissions)
            {
                if (!menuAccumulator.ContainsKey(permission.MenuId))
                {
                    var permissionMenu = _menuLst.First(e => e.Id == permission.MenuId);
                    var item = new MenuDto()
                    {
                        Id = permission.MenuId,
                        ParentId = permissionMenu.ParentId,
                        Name = permissionMenu.Name,
                        Url = permissionMenu.Url,
                        System = permissionMenu.System,
                        Index = permissionMenu.Index,
                        Depth = permissionMenu.Depth
                    };

                    menuAccumulator.Add(permission.MenuId, item);
                }
            }

            var subMenuHasParent = menuAccumulator.Where(e => !string.IsNullOrWhiteSpace(e.Value.ParentId))
                .Select(e => e.Value).ToArray();

            if (subMenuHasParent.Any())
            {
                GetParentMenu(subMenuHasParent, menuAccumulator);
            }

            var returnVal = menuAccumulator.Values.ToList();
            return returnVal;
        }

        /// <summary>
        /// 获取父菜单
        /// </summary>
        /// <param name="subMenu">子菜单</param>
        /// <param name="menuAccumulator">累加菜单</param>
        /// <param name="depth">深度</param>
        private void GetParentMenu(IEnumerable<MenuDto> subMenu, IDictionary<string, MenuDto> menuAccumulator)
        {
            foreach (var item in subMenu)
            {
                if (!menuAccumulator.ContainsKey(item.ParentId))
                {
                    var menu = (from e in _menuLst where (e.Id.Equals(item.ParentId)) select e).First();

                    var menuItem = new MenuDto
                    {
                        Id = menu.Id,
                        ParentId = menu.ParentId,
                        Name = menu.Name,
                        Url = menu.Url,
                        System = menu.System,
                        Index = menu.Index,
                        Depth = menu.Depth
                    };

                    menuAccumulator.Add(menuItem.Id, menuItem);
                }
            }

            var subMenuHasParent = menuAccumulator.Where(e => !string.IsNullOrWhiteSpace(e.Value.ParentId));
            var parentMenuNotInAccumulator = subMenuHasParent
                .Where(e => !menuAccumulator.ContainsKey(e.Value.ParentId))
                .Select(e => e.Value).ToArray();

            if (parentMenuNotInAccumulator.Any())
            {
                GetParentMenu(parentMenuNotInAccumulator, menuAccumulator);
            }
        }

        /// <summary>
        /// 获取用户所拥有的操作权限Id
        /// </summary>
        /// <param name="token"></param>
        /// <returns>用户所拥有的操作权限Id</returns>
        public string[] GetPermissionId(string token)
        {
            string[] permissionIdlst;
            _rwLocker.EnterReadLock();
            try
            {
                if (!_tokenToPermission.TryGetValue(token, out permissionIdlst))
                {
                    throw new IdentityNotMappedException("未能根据用户自定义标识关联到相应权限Id");
                }
            }
            finally
            {
                _rwLocker.ExitReadLock();
            }
            return permissionIdlst;
        }

        /// <summary>
        /// 验证用户角色是否拥有指定的操作权限
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <param name="token">角色Id列表</param>
        /// <returns>用户角色拥有指定操作权限返回true,否则反之</returns>
        public bool VerifyPermission(string permissionId, string token)
        {
            return GetPermissionId(token).Contains(permissionId);
        }

        /// <summary>
        /// 根据权限Id获取权限实例
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <returns>权限实例</returns>
        public Permission GetPermission(string permissionId)
        {
            var permission = _permissionLst.SingleOrDefault(e => e.Id == permissionId);
            if (permission != null)
            {
                return permission;
            }
            return null;
        }

        /// <summary>
        /// 根据Token获取权限列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns>权限列表</returns>
        private IEnumerable<Permission> GetPermissions(string token)
        {
            var permissionIdLst = GetPermissionId(token);
            return _permissionLst.Where(x => permissionIdLst.Contains(x.Id));
        }

        /// <summary>
        /// 重新设定角色的功能权限
        /// </summary>
        /// <param name="roldId">角色Id</param>
        public void RebuildFuncAndMenuDic(long roldId)
        {
            _rwLocker.EnterUpgradeableReadLock();
            try
            {
                var tokens = _tokenToRoleId.Where(x => x.Value.Contains(roldId)).Select(x => x.Key);
                foreach (var token in tokens)
                {
                    _rwLocker.EnterWriteLock();
                    try
                    {
                        _tokenToMenuItem.Remove(token);
                        _tokenToPermission.Remove(token);
                    }
                    finally
                    {
                        _rwLocker.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _rwLocker.ExitUpgradeableReadLock();
            }
        }

        public void SetTicket(string userName, string token, bool rememberMe)
        {
            var expiration = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.Add(FormsAuthentication.Timeout);
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, true, token, FormsAuthentication.FormsCookiePath);
            var hashTicket = FormsAuthentication.Encrypt(ticket);
            var userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            HttpContext.Current.Response.Cookies.Set(userCookie);
        }

        public void ClearTicket()
        {
            FormsAuthentication.SignOut();
        }
    }
}
