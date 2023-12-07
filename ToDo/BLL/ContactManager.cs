using System;
using System.Data;
using System.Text.RegularExpressions;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class ContactManager
    {
        public static bool InsertContact(Contact newContact)
        {
            if (!ContactRepository.IsContactExists(newContact.CID))
            {
                return ContactRepository.InsertContact(newContact);
            }
            return false;
        }

        public static bool UpdateContact(Contact updatedContact)
        {
            if (ContactRepository.IsContactExists(updatedContact.CID))
            {
                return ContactRepository.UpdateContact(updatedContact);
            }
            return false;
        }

        public static bool DeleteContact(string contactID)
        {
            if (ContactRepository.IsContactExists(contactID))
            {
                return ContactRepository.DeleteContactByID(contactID);
            }
            return false;
        }

        public static bool QueryAllContacts(string userID, out DataTable result)
        {
            return ContactRepository.QueryAllContacts(userID, out result);
        }

        public static bool GetEmailByName(string contactName, out string email)
        {
            return ContactRepository.QueryEmailByName(contactName, out email);
        }

        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+?[0-9]{1,4}?[-.\\s]?(\(\d{1,}\)|\d{1,})[-.\\s]?\d{1,}[-.\\s]?\d{1,}[-.\\s]?\d{1,}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
