using System;
using System.Data;
using System.Windows;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class UserManager
    {
        public static bool InsertUser(User newUser)
        {
            if (!UserRepository.IsUserExists(newUser.Email))
            {
                return UserRepository.InsertUser(newUser);
            }
            return false;
        }

        public static bool UpdateUser(User updatedUser)
        {
            if (UserRepository.IsUserExists(updatedUser.ID))
            {
                return UserRepository.UpdateUser(updatedUser);
            }
            return false;
        }

        public static bool DeleteUser(int userID)
        {
            if (UserRepository.IsUserExists(userID))
            {
                return UserRepository.DeleteUserByID(userID);
            }
            return false; 
        }

        public static bool QueryUser(string email, out DataTable result)
        {
            return UserRepository.QueryUserByEmail(email, out result);
        }

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
