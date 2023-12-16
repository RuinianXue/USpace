using System;
using System.Data;
using System.Windows;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class UserManager
    {
        /// <summary>
        /// 向数据库插入新用户信息
        /// </summary>
        /// <param name="newUser">新用户对象</param>
        /// <returns>插入是否成功</returns>
        public static bool InsertUser(User newUser)
        {
            if (!UserRepository.IsUserExists(newUser.UID))
            {
                return UserRepository.InsertUser(newUser);
            }
            return false;
        }

        /// <summary>
        /// 更新数据库中用户信息
        /// </summary>
        /// <param name="updatedUser">更新后的用户对象</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateUser(User updatedUser)
        {
            if (UserRepository.IsUserExists(updatedUser.UID))
            {
                return UserRepository.UpdateUser(updatedUser);
            }
            return false;
        }

        /// <summary>
        /// 从数据库中删除指定用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteUser(string userID)
        {
            if (UserRepository.IsUserExists(userID))
            {
                return UserRepository.DeleteUserByUID(userID);
            }
            return false;
        }

        /// <summary>
        /// 根据用户邮箱查询用户信息
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="result">查询结果的DataTable</param>
        /// <returns>是否查询到用户信息</returns>
        public static bool QueryUser(string email, out DataTable result)
        {
            return UserRepository.QueryUserByEmail(email, out result);
        }

        /// <summary>
        /// 根据用户邮箱查询用户ID
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="userID">查询到的用户ID</param>
        /// <returns>是否查询到用户ID</returns>
        public static bool QueryUserIDByEmail(string email, out string userID)
        {
            DataTable result;
            bool success = UserRepository.QueryUserIDByEmail(email, out result, out userID);
            return success;
        }

        /// <summary>
        /// 验证用户密码是否匹配
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <param name="password">用户输入的密码</param>
        /// <returns>密码是否匹配</returns>
        public static bool VerifyUserPassword(string email, string password)
        {
            DataTable result;
            if (UserRepository.QueryUserByEmail(email, out result))
            {
                string storedHashedPassword = result.Rows[0]["Password"].ToString();
                Console.WriteLine(password + " → " + storedHashedPassword);
                return PasswordManager.VerifyPassword(password, storedHashedPassword);
            }
            return false;
        }
    }
}
