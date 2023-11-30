using System;
using System.Data;
using System.Windows;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class UserManager
    {
        public static bool InsertUser(string newUserName, DateTime newDateOfBirth, string newEmail, string newPassword)
        {
            if (!UserRepository.IsUserExists(newEmail))
            {
                User user = new User
                {
                    Nickname = newUserName,
                    DateOfBirth = newDateOfBirth,
                    Email = newEmail,
                    Password = newPassword
                };

                return UserRepository.InsertUser(user);
            }
            return false; 
        }

        public static bool UpdateUser(int userID, string newUserName, DateTime newDateOfBirth, string newEmail, string newPassword)
        {
            if (UserRepository.IsUserExists(userID))
            {
                User user = new User
                {
                    ID = userID,
                    Nickname = newUserName,
                    DateOfBirth = newDateOfBirth,
                    Email = newEmail,
                    Password = newPassword
                };

                return UserRepository.UpdateUser(user);
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

        public static bool SearchUser(string email, out DataTable result)
        {
            return UserRepository.SearchUserByEmail(email, out result);
        }

        public static bool VerifyUserPassword(string email, string password)
        {
            DataTable result;
            if (UserRepository.SearchUserByEmail(email, out result))
            {
                string storedHashedPassword = result.Rows[0]["Password"].ToString();
                Console.WriteLine(password + " → " + storedHashedPassword);
                return PasswordManager.VerifyPassword(password, storedHashedPassword);
            }
            return false;
        }
    }
}
