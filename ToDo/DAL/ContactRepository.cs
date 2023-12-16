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

        /// <summary>
        /// 向数据库插入联系人信息
        /// </summary>
        /// <param name="contact">联系人对象</param>
        /// <returns>插入是否成功</returns>
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

        /// <summary>
        /// 更新数据库中的联系人信息
        /// </summary>
        /// <param name="contact">联系人对象</param>
        /// <returns>更新是否成功</returns>
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

        /// <summary>
        /// 根据联系人ID删除数据库中的联系人信息
        /// </summary>
        /// <param name="contactID">联系人ID</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteContactByID(string contactID)
        {
            string sql = "DELETE FROM Contact WHERE CID=@cid";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@cid", contactID)
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        /// <summary>
        /// 查询数据库中指定用户的所有联系人信息
        /// </summary>
        /// <param name="userID">用户唯一标识符</param>
        /// <param name="result">查询结果的DataTable</param>
        /// <returns>是否查询到联系人信息</returns>
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

        /// <summary>
        /// 根据联系人姓名查询邮箱信息
        /// </summary>
        /// <param name="contactName">联系人姓名</param>
        /// <param name="email">查询到的邮箱信息</param>
        /// <returns>是否查询到邮箱信息</returns>
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

        /// <summary>
        /// 判断联系人是否存在
        /// </summary>
        /// <param name="CID">联系人ID</param>
        /// <returns>是否存在</returns>
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
