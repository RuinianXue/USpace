using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace UIDisplay.BLL
{
    public class PasswordManager
    {
        /// <summary>
        /// 生成密码哈希
        /// </summary>
        /// <param name="password">要哈希的密码</param>
        /// <returns>生成的密码哈希</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">用户输入的密码</param>
        /// <param name="hashedPassword">存储在数据库中的密码哈希</param>
        /// <returns>密码是否匹配</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
