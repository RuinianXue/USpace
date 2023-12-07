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
            string insertQuery = "INSERT INTO User (UID, Nickname, DateOfBirth, Email, Password, JsonFilePath) VALUES (@uid, @nickname, @dob, @email, @password, @jsonFilePath)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@uid", user.UID),
                new MySqlParameter("@nickname", user.Nickname),
                new MySqlParameter("@dob", user.DateOfBirth),
                new MySqlParameter("@email", user.Email),
                new MySqlParameter("@password", PasswordManager.HashPassword(user.Password)),
                new MySqlParameter("@jsonFilePath", user.JsonFilePath) 
            };

            return mysqlBase.CommonExecute(insertQuery, parameters) > 0;
        }

        public static bool UpdateUser(User user)
        {
            string query = "UPDATE User SET Nickname = @nickname, DateOfBirth = @dob, Email = @email, Password = @password, JsonFilePath = @jsonFilePath WHERE UID = @uid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@nickname", user.Nickname),
                new MySqlParameter("@dob", user.DateOfBirth),
                new MySqlParameter("@email", user.Email),
                new MySqlParameter("@password", PasswordManager.HashPassword(user.Password)),
                new MySqlParameter("@jsonFilePath", user.JsonFilePath), 
                new MySqlParameter("@uid", user.UID)
            };

            return mysqlBase.CommonExecute(query, parameters) > 0;
        }

        public static bool DeleteUserByUID(string uid)
        {
            string query = "DELETE FROM User WHERE UID = @uid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@uid", uid)
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

        public static bool QueryUserIDByEmail(string email, out DataTable result, out string userID)
        {
            string query = "SELECT UID FROM User WHERE Email = @userEmail";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@userEmail", email)
            };

            result = mysqlBase.Query(query, parameters);

            if (result.Rows.Count > 0)
            {
                userID = Convert.ToString(result.Rows[0]["UID"]);
                return true;
            }

            userID = null;
            return false;
        }

        public static bool IsUserExists(string uid)
        {
            string query = "SELECT COUNT(*) FROM User WHERE UID = @uid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@uid", uid)
            };

            int count = mysqlBase.CommonExecute(query, parameters);
            return count > 0;
        }
    }
}
