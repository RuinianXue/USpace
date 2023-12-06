using System;
using System.Data;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class ContactManager
    {
        public static bool InsertContact(Contact newContact)
        {
            if (!ContactRepository.IsContactExists(newContact.Email))
            {
                return ContactRepository.InsertContact(newContact);
            }
            return false;
        }

        public static bool UpdateContact(Contact updatedContact)
        {
            if (ContactRepository.IsContactExists(updatedContact.Email))
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

        public static bool SearchAllContact(out DataTable result)
        {
            return ContactRepository.SearchAllContact(out result);
        }
    }
}
