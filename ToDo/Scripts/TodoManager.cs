using HandyControl.Controls;
using System;
using System.Data;
using UIDisplay.Model;

namespace UIDisplay.Myscripts
{
    internal class TodoManager
    {
        private MysqlBase mysqlBase;

        public TodoManager()
        {
            mysqlBase = new MysqlBase();
        }

        public void InsertTodoInfo(TodoInfo todoInfo)
        {
            string sql = "INSERT INTO todoinfo (uuid, content, date, priority, isdone, teammate) VALUES (@UUID, @Content, @Date, @Priority, @IsDone, @Teammate)";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@UUID", todoInfo.UUID),
                new MySql.Data.MySqlClient.MySqlParameter("@Content", todoInfo.Content),
                new MySql.Data.MySqlClient.MySqlParameter("@Date", todoInfo.Date),
                new MySql.Data.MySqlClient.MySqlParameter("@Priority", todoInfo.Priority),
                new MySql.Data.MySqlClient.MySqlParameter("@IsDone", todoInfo.IsDone),
                new MySql.Data.MySqlClient.MySqlParameter("@Teammate", todoInfo.Teammate),
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

        public void UpdateTodoInfo(TodoInfo todoInfo)
        {
            string sql = "UPDATE todoinfo SET content = @Content, date = @Date, priority = @Priority, isdone = @IsDone, teammate = @Teammate WHERE uuid = @UUID";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@Content", todoInfo.Content),
                new MySql.Data.MySqlClient.MySqlParameter("@Date", todoInfo.Date),
                new MySql.Data.MySqlClient.MySqlParameter("@Priority", todoInfo.Priority),
                new MySql.Data.MySqlClient.MySqlParameter("@IsDone", todoInfo.IsDone),
                new MySql.Data.MySqlClient.MySqlParameter("@Teammate", todoInfo.Teammate),
                new MySql.Data.MySqlClient.MySqlParameter("@UUID", todoInfo.UUID),
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

        public void DeleteTodoInfo(TodoInfo todoInfo)
        {
            string sql = "DELETE FROM todoinfo WHERE uuid = @UUID";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@UUID", todoInfo.UUID),
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

        public DataTable QueryTodoInfo()
        {
            string sql = "SELECT uuid, content, date, priority, isdone, teammate FROM todoinfo ORDER BY priority DESC, date";
            DataSet ds = mysqlBase.GetDataSet(sql, "todoinfo");
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count != 0)
            {
                Growl.Success("云端数据拉取成功");
                return dt;
            }
            else
            {
                Growl.Info("暂无数据");
                return dt;
            }
        }
    }
}
