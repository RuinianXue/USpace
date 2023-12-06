using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using UIDisplay.Model;
using UIDisplay.Utils;

namespace UIDisplay.DAL
{
    public class ContactRepository
    {
        private static MysqlBase mysqlBase = new MysqlBase();

        public static bool InsertContact(Contact contact)
        {
            string sql = "INSERT INTO contact (uuid, name, phone, email, imgpath) VALUES (@uuid, @name, @phone, @email, @imgpath)";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@uuid", contact.CID),
                new MySqlParameter("@name", contact.Name),
                new MySqlParameter("@phone", contact.Phone),
                new MySqlParameter("@email", contact.Email),
                new MySqlParameter("@imgpath", contact.ImgPath)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool UpdateContact(Contact contact)
        {
            string sql = "UPDATE contact SET name=@name, phone=@phone, email=@email, imgpath=@imgpath WHERE uuid=@uuid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name", contact.Name),
                new MySqlParameter("@phone", contact.Phone),
                new MySqlParameter("@email", contact.Email),
                new MySqlParameter("@imgpath", contact.ImgPath),
                new MySqlParameter("@uuid", contact.CID)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool DeleteContactByID(string contactID)
        {
            string sql = "DELETE FROM contact WHERE uuid=@uuid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@uuid", contactID)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool SearchAllContact(out DataTable result)
        {
            string query = "SELECT uuid, name, phone, email, imgpath FROM contact";

            result = mysqlBase.Query(query);

            if (result.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static bool IsContactExists(string email)
        {
            string query = "SELECT COUNT(*) FROM contact WHERE Email = @contactEmail";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
        new MySqlParameter("@contactEmail", email)
            };

            int count = mysqlBase.CommonExecute(query, parameters);
            return count > 0;
        }
    }
}
