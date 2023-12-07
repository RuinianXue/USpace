using System.Data;
using System.Threading.Tasks;
using UIDisplay.Model;
using UIDisplay.Utils;

namespace UIDisplay.DAL
{
    public class TodoRepository
    {
        private static MysqlBase mysqlBase = new MysqlBase();

        public static bool InsertTodo(Todo todo)
        {
            string sql = "INSERT INTO Todo (TID, Content, Date, Priority, IsDone, Teammate, UID) VALUES (@TID, @Content, @Date, @Priority, @IsDone, @Teammate, @UID)";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@TID", todo.TID),
                new MySql.Data.MySqlClient.MySqlParameter("@Content", todo.Content),
                new MySql.Data.MySqlClient.MySqlParameter("@Date", todo.Date),
                new MySql.Data.MySqlClient.MySqlParameter("@Priority", todo.Priority),
                new MySql.Data.MySqlClient.MySqlParameter("@IsDone", todo.IsDone),
                new MySql.Data.MySqlClient.MySqlParameter("@Teammate", todo.Teammate),
                new MySql.Data.MySqlClient.MySqlParameter("@UID", todo.UID), 
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool UpdateTodo(Todo todo)
        {
            string sql = "UPDATE Todo SET Content = @Content, Date = @Date, Priority = @Priority, IsDone = @IsDone, Teammate = @Teammate, UID = @UID WHERE TID = @TID";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@Content", todo.Content),
                new MySql.Data.MySqlClient.MySqlParameter("@Date", todo.Date),
                new MySql.Data.MySqlClient.MySqlParameter("@Priority", todo.Priority),
                new MySql.Data.MySqlClient.MySqlParameter("@IsDone", todo.IsDone),
                new MySql.Data.MySqlClient.MySqlParameter("@Teammate", todo.Teammate),
                new MySql.Data.MySqlClient.MySqlParameter("@UID", todo.UID),
                new MySql.Data.MySqlClient.MySqlParameter("@TID", todo.TID),
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool DeleteTodo(Todo todo)
        {
            string sql = "DELETE FROM Todo WHERE TID = @TID";
            var parameters = new MySql.Data.MySqlClient.MySqlParameter[]
            {
                new MySql.Data.MySqlClient.MySqlParameter("@TID", todo.TID),
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static async Task<DataTable> QueryTodoAsync()
        {
            string sql = "SELECT TID, Content, Date, Priority, IsDone, Teammate FROM Todo ORDER BY Priority DESC, Date";
            DataSet dataset = await mysqlBase.GetDataSetAsync(sql, "todoinfo");
            return dataset.Tables[0];
        }
    }
}
