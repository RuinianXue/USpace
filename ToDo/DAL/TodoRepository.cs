using System;
using System.Data;
using UIDisplay.Model;
using UIDisplay.Utils;

namespace UIDisplay.DAL
{
    public class TodoRepository
    {
        private static MysqlBase mysqlBase = new MysqlBase();

        public static int InsertTodo(Todo todo)
        {
            string sql = "INSERT INTO todo (uuid, content, date, priority, isdone, teammate) VALUES (@UUID, @Content, @Date, @Priority, @IsDone, @Teammate)";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@UUID", todo.UUID),
                new MySql.Data.MySqlClient.MySqlParameter("@Content", todo.Content),
                new MySql.Data.MySqlClient.MySqlParameter("@Date", todo.Date),
                new MySql.Data.MySqlClient.MySqlParameter("@Priority", todo.Priority),
                new MySql.Data.MySqlClient.MySqlParameter("@IsDone", todo.IsDone),
                new MySql.Data.MySqlClient.MySqlParameter("@Teammate", todo.Teammate),
            };

            return mysqlBase.CommonExecute(sql, parameters);
        }

        public static int UpdateTodo(Todo todo)
        {
            string sql = "UPDATE todo SET content = @Content, date = @Date, priority = @Priority, isdone = @IsDone, teammate = @Teammate WHERE uuid = @UUID";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@Content", todo.Content),
                new MySql.Data.MySqlClient.MySqlParameter("@Date", todo.Date),
                new MySql.Data.MySqlClient.MySqlParameter("@Priority", todo.Priority),
                new MySql.Data.MySqlClient.MySqlParameter("@IsDone", todo.IsDone),
                new MySql.Data.MySqlClient.MySqlParameter("@Teammate", todo.Teammate),
                new MySql.Data.MySqlClient.MySqlParameter("@UUID", todo.UUID),
            };

            return mysqlBase.CommonExecute(sql, parameters);
        }

        public static int DeleteTodo(Todo todo)
        {
            string sql = "DELETE FROM todo WHERE uuid = @UUID";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@UUID", todo.UUID),
            };

            return mysqlBase.CommonExecute(sql, parameters);
        }

        public static DataTable QueryTodo()
        {
            string sql = "SELECT uuid, content, date, priority, isdone, teammate FROM todo ORDER BY priority DESC, date";
            DataSet ds = mysqlBase.GetDataSet(sql, "todoinfo");
            return ds.Tables[0];
        }
    }
}
