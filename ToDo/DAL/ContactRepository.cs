using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using UIDisplay.Model;
using UIDisplay.Utils;
using UIDisplay.BLL;

namespace UIDisplay.DAL
{
    public class ContactRepository
    {
        private static MysqlBase mysqlBase = new MysqlBase();

        public static bool InsertContact(Contact contact)
        {
            string sql = "INSERT INTO Contact (CID, Name, Phone, Email, ImgPath, UID) VALUES (@cid, @name, @phone, @email, @imgpath, @uid)";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cid", contact.CID),
                new MySqlParameter("@name", contact.Name),
                new MySqlParameter("@phone", contact.Phone),
                new MySqlParameter("@email", contact.Email),
                new MySqlParameter("@imgpath", contact.ImgPath),
                new MySqlParameter("@uid", contact.UID)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool UpdateContact(Contact contact)
        {
            string sql = "UPDATE Contact SET Name=@name, Phone=@phone, Email=@email, UID=@uid, ImgPath=@imgpath WHERE CID=@cid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name", contact.Name),
                new MySqlParameter("@phone", contact.Phone),
                new MySqlParameter("@email", contact.Email),
                new MySqlParameter("@imgpath", contact.ImgPath),
                new MySqlParameter("@uid", contact.UID),
                new MySqlParameter("@cid", contact.CID)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool DeleteContactByID(string contactID)
        {
            string sql = "DELETE FROM Contact WHERE CID=@cid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cid", contactID)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool QueryAllContacts(string userID, out DataTable result)
        {
            string query = "SELECT CID, Name, Phone, Email, UID, ImgPath FROM Contact WHERE UID=@UID";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@UID", userID)
            };

            result = mysqlBase.Query(query, parameters);

            if (result.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static bool QueryEmailByName(string contactName, out string email)
        {
            string query = "SELECT Email FROM Contact WHERE Name=@name";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name", contactName)
            };

            DataTable result = mysqlBase.Query(query, parameters);

            if (result.Rows.Count > 0)
            {
                email = result.Rows[0]["Email"].ToString();
                return true;
            }

            email = null;
            return false;
        }

        public static bool IsContactExists(string CID)
        {
            string query = "SELECT COUNT(*) FROM Contact WHERE CID=@cid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cid", CID)
            };

            int count = mysqlBase.CommonExecute(query, parameters);
            return count > 0;
        }
    }
}
