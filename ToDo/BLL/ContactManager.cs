using HandyControl.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using UIDisplay.Model;
using UIDisplay.Utils;

namespace UIDisplay.BLL
{
    internal class ContactManager
    {
        private MysqlBase mysqlBase;

        public ContactManager()
        {
            mysqlBase = new MysqlBase();
        }

        public void InsertUserInfo(Contact userInfo)
        {
            string sql = "INSERT INTO userinfo (uuid, name, phone, email, imgpath) VALUES (@uuid, @name, @phone, @email, @imgpath)";
            Console.WriteLine(sql);

            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", userInfo.UUID),
                new MySqlParameter("@name", userInfo.Name),
                new MySqlParameter("@phone", userInfo.PhoneNum),
                new MySqlParameter("@email", userInfo.Email),
                new MySqlParameter("@imgpath", userInfo.ImgPath)
            };

            int res = mysqlBase.CommonExecute(sql, parameters);

            if (res > 0)
            {
                Growl.Success("云端数据添加成功！");
            }
            else
            {
                Growl.Warning("云端数据添加失败！");
            }
        }

        public void UpdateUserInfo(Contact userInfo)
        {
            string sql = "UPDATE userinfo SET name=@name, phone=@phone, email=@email, imgpath=@imgpath WHERE uuid=@uuid";
            Console.WriteLine(sql);

            MySqlParameter[] parameters = {
                new MySqlParameter("@name", userInfo.Name),
                new MySqlParameter("@phone", userInfo.PhoneNum),
                new MySqlParameter("@email", userInfo.Email),
                new MySqlParameter("@imgpath", userInfo.ImgPath),
                new MySqlParameter("@uuid", userInfo.UUID)
            };

            int res = mysqlBase.CommonExecute(sql, parameters);

            if (res > 0)
            {
                Growl.Success("云端数据更新成功！");
            }
            else
            {
                Growl.Warning("云端数据更新失败！");
            }
        }

        public void DeleteUserInfo(Contact userInfo)
        {
            string sql = "DELETE FROM userinfo WHERE uuid=@uuid";
            Console.WriteLine(sql);

            MySqlParameter[] parameters = {
                new MySqlParameter("@uuid", userInfo.UUID)
            };

            int res = mysqlBase.CommonExecute(sql, parameters);

            if (res > 0)
            {
                Growl.Success("云端数据删除成功！");
            }
            else
            {
                Growl.Warning("云端数据删除失败！");
            }
        }

        public DataTable QueryUserInfo()
        {
            string sql = "SELECT uuid, name, phone, email, imgpath FROM userinfo";
            Console.WriteLine(sql);

            DataSet ds = mysqlBase.GetDataSet(sql, "userinfo");
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count != 0)
            {
                Growl.Success("云端数据拉取成功");
                return dt;
            }
            else
            {
                Growl.Info("暂无数据");
            }
            return dt;
        }
    }
}
