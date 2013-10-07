using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Util.Encryption
{
    public class HashFunctionEncryption
    {
        /// <summary>
        /// 哈希函数加密
        /// </summary>
        /// <param name="source">待加密的数据源</param>
        /// <param name="salt">盐值，默认不加盐</param>
        /// <returns>加密后的字符串</returns>
        public static string Encode(string source, string salt = "")
        {
            var passwordAndSaltBytes = Encoding.UTF8.GetBytes(source + salt);
            var hashBytes = new SHA256Managed().ComputeHash(passwordAndSaltBytes);
            var hashString = Convert.ToBase64String(hashBytes);
            return hashString;
        }

        /// <summary>
        /// 哈希函数加密
        /// </summary>
        /// <param name="source">待加密的数据源</param>
        /// <param name="salt">盐值，默认不加盐</param>
        /// <returns>加密后的字符串</returns>
        public static string Encode(string source, Guid? salt)
        {
            salt = salt ?? Guid.NewGuid();
            return Encode(source, salt.ToString());
        }
    }
}
