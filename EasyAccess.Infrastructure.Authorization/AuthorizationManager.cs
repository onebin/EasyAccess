using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Repository.Configuration;
using EasyAccess.Repository.IRepositories;
using EasyAccess.Repository.Repositories;

namespace EasyAccess.Infrastructure.Authorization
{
    public class AuthorizationManager
    {
        private static AuthorizationManager _instance = null;
        private static readonly object Locker = new object();

        private readonly List<Menu> _menuDic = new List<Menu>();
        private readonly List<Permission> _permissionDic = new List<Permission>();

        private readonly Dictionary<string, List<MenuItem>> _userMenuItemDic = new Dictionary<string, List<MenuItem>>();
        private readonly Dictionary<string, string[]> _userFuncDic = new Dictionary<string, string[]>();
        private readonly Dictionary<string, List<Role>> _tokenDic = new Dictionary<string, List<Role>>();

        private AuthorizationManager()
        {
            var ctx = new EasyAccessContext();
            _menuDic = ctx.Menus.ToList();
            _permissionDic = ctx.Permissions.ToList();
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
        /// <returns></returns>
        public string GetToken(IEnumerable<Role> roleList)
        {
            string token = string.Empty;
            var roleIdLst = roleList.OrderBy(x => x.Id).Select(x => x.Id).ToArray();
            var dividerCount = SessionConst.TokenDivider.Length;
            for (int i = 0; i < roleIdLst.Count(); i++)
            {
                token += SessionConst.TokenDivider[i%dividerCount] + roleIdLst[i];
            }
            if (!_tokenDic.ContainsKey(token))
            {
                _tokenDic.Add(token, roleList.ToList());
            }

            return token;
        }

        /// <summary>
        /// 获取角色Id列表
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>角色Id列表</returns>
        private long[] GetRoleIdLst(string token)
        {
            return token.Split(SessionConst.TokenDivider, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>用户菜单项</returns>
        public List<MenuItem> GetMenu(string token)
        {
            List<MenuItem> returnVal = null;
            if (!_userMenuItemDic.TryGetValue(token, out returnVal))
            {
                returnVal = BuildMenu(token);
                _userMenuItemDic.Add(token, returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <param name="token">用户角色标识</param>
        /// <returns>菜单项</returns>
        public List<MenuItem> GetSubMenu(string menuId, string token)
        {
            List<MenuItem> returnVal = null;
            returnVal = _userMenuItemDic.First(e => e.Key == token).Value
                .Where(e => e.ParentId == menuId).Select(e => e).ToList<MenuItem>();
            return returnVal;
        }

        /// <summary>
        /// 建立菜单字典并返回用户菜单项
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>用户菜单项</returns>
        private List<MenuItem> BuildMenu(string token)
        {
            var menuAccumulator = new Dictionary<string, MenuItem>();

            var permissions = GetPermissions(token);

            foreach (var permission in permissions)
            {
                if (!menuAccumulator.ContainsKey(permission.MenuId))
                {
                    var permissionMenu = _menuDic.First(e => e.Id == permission.MenuId);
                    var item = new MenuItem()
                    {
                        Id = permission.MenuId,
                        ParentId = permissionMenu.ParentId,
                        Name = permissionMenu.Name,
                        Url = permissionMenu.Url,
                        System = permissionMenu.System,
                        Index = permissionMenu.Index,
                        Depth = 0
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
        private void GetParentMenu(IEnumerable<MenuItem> subMenu, IDictionary<string, MenuItem> menuAccumulator, int depth = 0)
        {
            depth = ++depth;
            foreach (var item in subMenu)
            {
                if (!menuAccumulator.ContainsKey(item.ParentId))
                {
                    var menu = (from e in _menuDic where (e.Id.Equals(item.ParentId)) select e).First();

                    var menuItem = new MenuItem();
                    menuItem.Id = menu.Id;
                    menuItem.ParentId = menu.ParentId;
                    menuItem.Name = menu.Name;
                    menuItem.Url = menu.Url;
                    menuItem.System = menu.System;
                    menuItem.Index = menu.Index;
                    menuItem.Depth = depth;

                    menuAccumulator.Add(menuItem.Id, menuItem);
                }
            }

            var subMenuHasParent = menuAccumulator.Where(e => e.Value.ParentId != string.Empty);
            var parentMenuNotInAccumulator = subMenuHasParent
                .Where(e => !menuAccumulator.ContainsKey(e.Value.ParentId))
                .Select(e => e.Value).ToArray();

            if (parentMenuNotInAccumulator.Any())
            {
                GetParentMenu(parentMenuNotInAccumulator, menuAccumulator, depth);
            }
        }

        /// <summary>
        /// 获取用户所拥有的操作权限Id
        /// </summary>
        /// <param name="token"></param>
        /// <returns>用户所拥有的操作权限Id</returns>
        public string[] GetFunc(string token)
        {
            string[] func = null;
            if (!_userFuncDic.TryGetValue(token, out func))
            {
                func = BuildFunc(token);
                _userFuncDic.Add(token, func);
            }
            return func;
        }

        /// <summary>
        /// 建立权限字典并返回用户操作权限Id列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns>用户所拥有的操作权限Id</returns>
        private string[] BuildFunc(string token)
        {
            IList<string> funcAccumulator = new List<string>();

            var permissions = GetPermissions(token);

            foreach (var permission in permissions)
            {
                if (!funcAccumulator.Contains(permission.Id))
                {
                    funcAccumulator.Add(permission.Id);
                }
            }
            return funcAccumulator.ToArray();
        }

        /// <summary>
        /// 验证用户角色是否拥有指定的操作权限
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <param name="token">角色Id列表</param>
        /// <returns>用户角色拥有指定操作权限返回true,否则反之</returns>
        public bool VerifyPermission(string permissionId, string token)
        {
            return GetFunc(token).Contains(permissionId);
        }

        /// <summary>
        /// 根据权限Id获取权限实例
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <returns>权限实例</returns>
        public Permission GetPermission(string permissionId)
        {
            var permission = _permissionDic.SingleOrDefault(e => e.Id == permissionId);
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
            IRoleRepository ropo = new RoleRepository(new EasyAccessContext());
            var roleIdLst = GetRoleIdLst(token);
            return ropo.GetPermissions(roleIdLst);
        }

        /// <summary>
        /// 重新设定角色的功能权限
        /// </summary>
        /// <param name="roldId">角色Id</param>
        public void RebuildFuncAndMenuDic(long roldId)
        {
            var userMenuDicRebuildItem = _userMenuItemDic.Where(e => e.Key.Split(SessionConst.TokenDivider, StringSplitOptions.RemoveEmptyEntries).Contains(roldId.ToString(CultureInfo.InvariantCulture))).Select(e => e.Key).ToArray();
            var userFuncDicRebuildItem = _userFuncDic.Where(e => e.Key.Split(SessionConst.TokenDivider, StringSplitOptions.RemoveEmptyEntries).Contains(roldId.ToString(CultureInfo.InvariantCulture))).Select(e => e.Key).ToArray();

            foreach (var removeItem in userMenuDicRebuildItem)
            {
                _userMenuItemDic.Remove(removeItem);
            }
            foreach (var removeItem in userFuncDicRebuildItem)
            {
                _userFuncDic.Remove(removeItem);
            }
        }
    }
}
