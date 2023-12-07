using MySql.Data.MySqlClient;
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
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@TID", todo.TID),
                new MySqlParameter("@Content", todo.Content),
                new MySqlParameter("@Date", todo.Date),
                new MySqlParameter("@Priority", todo.Priority),
                new MySqlParameter("@IsDone", todo.IsDone),
                new MySqlParameter("@Teammate", todo.Teammate),
                new MySqlParameter("@UID", todo.UID), 
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool UpdateTodo(Todo todo)
        {
            string sql = "UPDATE Todo SET Content = @Content, Date = @Date, Priority = @Priority, IsDone = @IsDone, Teammate = @Teammate, UID = @UID WHERE TID = @TID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Content", todo.Content),
                new MySqlParameter("@Date", todo.Date),
                new MySqlParameter("@Priority", todo.Priority),
                new MySqlParameter("@IsDone", todo.IsDone),
                new MySqlParameter("@Teammate", todo.Teammate),
                new MySqlParameter("@UID", todo.UID),
                new MySqlParameter("@TID", todo.TID),
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static bool DeleteTodo(Todo todo)
        {
            string sql = "DELETE FROM Todo WHERE TID = @TID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@TID", todo.TID),
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        public static async Task<DataTable> QueryTodoAsync(string userID)
        {
            string sql = "SELECT TID, Content, Date, Priority, IsDone, Teammate FROM Todo WHERE UID = @UID ORDER BY Priority DESC, Date";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@UID", userID)
            };

            DataSet dataset = await mysqlBase.GetDataSetAsync(sql, "todo", parameters);
            return dataset.Tables[0];
        }
    }
}
