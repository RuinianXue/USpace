using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class LoginManager
    {
        /// <summary>
        /// 当前用户ID，仅用于测试目的
        /// </summary>
        public static string CurrentUserID { get; private set; } = "a241898c-a7d9-41bd-b390-6b6194e58d21";

        /// <summary>
        /// 执行登录操作
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns>登录是否成功</returns>
        public static bool PerformLogin(string email)
        {
            string userID;
            if (UserManager.QueryUserIDByEmail(email, out userID))
            {
                CurrentUserID = userID;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证电子邮件格式是否正确
        /// </summary>
        /// <param name="email">待验证的电子邮件</param>
        /// <returns>电子邮件格式是否正确</returns>
        public static bool ValidateEmail(string email)
        {
            // 使用简单的正则表达式模式进行电子邮件验证
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// 检查电子邮件是否已注册
        /// </summary>
        /// <param name="email">待检查的电子邮件</param>
        /// <returns>电子邮件是否已注册</returns>
        public static bool CheckIfEmailIsRegistered(string email)
        {
            DataTable result;
            return UserManager.QueryUser(email, out result);
        }

        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="password">用户密码</param>
        /// <returns>密码是否验证成功</returns>
        public static bool VerifyPassword(string email, string password)
        {
            return UserManager.VerifyUserPassword(email, password);
        }
    }
}
