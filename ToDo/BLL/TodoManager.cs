using System.Data;
using UIDisplay.Model;
using UIDisplay.DAL;

namespace UIDisplay.BLL
{
    public class TodoManager
    {
        public static int InsertTodoInfo(Todo todo)
        {
            int res = TodoRepository.InsertTodo(todo);
            return res;
        }

        public static int UpdateTodoInfo(Todo todo)
        {
            int res = TodoRepository.UpdateTodo(todo);
            return res;
        }

        public static int DeleteTodoInfo(Todo todo)
        {
            int res = TodoRepository.DeleteTodo(todo);
            return res;
        }

        public static DataTable QueryTodoInfo()
        {
            return TodoRepository.QueryTodo();
        }
    }
}
