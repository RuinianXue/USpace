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
            // Check if the user exists; if not, perform the insert operation
            if (!UserRepository.IsUserExists(newEmail))
            {
                // Call the DAL method to execute the insert operation
                User user = new User
                {
                    Nickname = newUserName,
                    DateOfBirth = newDateOfBirth,
                    Email = newEmail,
                    Password = newPassword
                };

                return UserRepository.InsertUser(user);
            }
            return false; // User exists, no insert operation performed
        }

        public static bool UpdateUser(int userID, string newUserName, DateTime newDateOfBirth, string newEmail, string newPassword)
        {
            // Check if the user exists; if so, perform the update operation
            if (UserRepository.IsUserExists(userID))
            {
                // Call the DAL method to execute the update operation
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
            return false; // User does not exist, no update operation performed
        }

        public static bool DeleteUser(int userID)
        {
            // Check if the user exists; if so, perform the delete operation
            if (UserRepository.IsUserExists(userID))
            {
                // Call the DAL method to execute the delete operation
                return UserRepository.DeleteUserByID(userID);
            }
            return false; // User does not exist, no delete operation performed
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
