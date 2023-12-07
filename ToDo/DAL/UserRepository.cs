using System;
using System.Data;
using MySql.Data.MySqlClient;
using UIDisplay.Model;
using UIDisplay.Utils;
using UIDisplay.BLL;

namespace UIDisplay.DAL
{
    public class UserRepository
    {
        private static MysqlBase mysqlBase = new MysqlBase();

        public static bool InsertUser(User user)
        {
            string insertQuery = "INSERT INTO User (Nickname, DateOfBirth, Email, Password) VALUES (@nickname, @dob, @email, @password)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@nickname", user.Nickname),
                new MySqlParameter("@dob", user.DateOfBirth), 
                new MySqlParameter("@email", user.Email),
                new MySqlParameter("@password", PasswordManager.HashPassword(user.Password))
            };

            return mysqlBase.CommonExecute(insertQuery, parameters) > 0;
        }

        public static bool UpdateUser(User user)
        {
            string query = "UPDATE User SET Nickname = @nickname, DateOfBirth = @dob, Email = @email, Password = @password WHERE ID = @userID";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@nickname", user.Nickname),
                new MySqlParameter("@dob", user.DateOfBirth),
                new MySqlParameter("@email", user.Email),
                new MySqlParameter("@password", PasswordManager.HashPassword(user.Password)),
                new MySqlParameter("@userID", user.UID)
            };

            return mysqlBase.CommonExecute(query, parameters) > 0;
        }


        public static bool DeleteUserByID(int userID)
        {
            string query = "DELETE FROM User WHERE ID = @userID";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userID", userID)
            };

            return mysqlBase.CommonExecute(query, parameters) > 0;
        }

        public static bool QueryUserByEmail(string email, out DataTable result)
        {
            string query = "SELECT * FROM User WHERE Email = @userEmail";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userEmail", email)
            };

            result = mysqlBase.Query(query, parameters);

            if (result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsUserExists(int userID)
        {
            string query = "SELECT COUNT(*) FROM User WHERE ID = @userID";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userID", userID)
            };

            int count = mysqlBase.CommonExecute(query, parameters);
            return count > 0;
        }

        public static bool IsUserExists(string email)
        {
            string query = "SELECT COUNT(*) FROM User WHERE Email = @userEmail";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userEmail", email)
            };

            int count = mysqlBase.CommonExecute(query, parameters);
            return count > 0;
        }
    }
}
