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
        public static string CurrentUserID { get; private set; } = "a241898c-a7d9-41bd-b390-6b6194e58d21";  //待修改：仅测试用途

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

        public static bool ValidateEmail(string email)
        {
            // Use a simple regex pattern for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool CheckIfEmailIsRegistered(string email)
        {
            DataTable result;
            return UserManager.QueryUser(email, out result);
        }

        public static bool VerifyPassword(string email, string password)
        {
            return UserManager.VerifyUserPassword(email, password);
        }
    }
}
