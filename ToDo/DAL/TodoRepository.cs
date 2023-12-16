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

        /// <summary>
        /// 向数据库插入待办事项信息
        /// </summary>
        /// <param name="todo">待办事项对象</param>
        /// <returns>插入是否成功</returns>
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

        /// <summary>
        /// 更新数据库中的待办事项信息
        /// </summary>
        /// <param name="todo">待办事项对象</param>
        /// <returns>更新是否成功</returns>
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

        /// <summary>
        /// 删除数据库中的待办事项信息
        /// </summary>
        /// <param name="todo">待办事项对象</param>
        /// <returns>删除是否成功</returns>
        public static bool DeleteTodo(Todo todo)
        {
            string sql = "DELETE FROM Todo WHERE TID = @TID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@TID", todo.TID),
            };

            return mysqlBase.CommonExecute(sql, parameters) > 0;
        }

        /// <summary>
        /// 异步查询数据库中的待办事项信息
        /// </summary>
        /// <param name="userID">用户唯一标识符</param>
        /// <returns>待办事项信息的DataTable</returns>
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
