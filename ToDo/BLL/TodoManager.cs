using System.Data;
using System.Threading.Tasks;
using UIDisplay.DAL;
using UIDisplay.Model;

namespace UIDisplay.BLL
{
    public class TodoManager
    {
        public static bool InsertTodo(Todo todo)
        {
            return TodoRepository.InsertTodo(todo);
        }

        public static bool UpdateTodo(Todo todo)
        {
            return TodoRepository.UpdateTodo(todo);
        }

        public static bool DeleteTodo(Todo todo)
        {
            return TodoRepository.DeleteTodo(todo);
        }

        public static async Task<DataTable> QueryTodoAsync()
        {
            return await TodoRepository.QueryTodoAsync();
        }
    }
}
