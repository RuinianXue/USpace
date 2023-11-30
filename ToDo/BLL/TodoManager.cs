using System.Data;
using UIDisplay.Model;
using UIDisplay.DAL;

namespace UIDisplay.BLL
{
    public class TodoManager
    {
        public int InsertTodoInfo(Todo todo)
        {
            int res = TodoRepository.InsertTodo(todo);
            return res;
        }

        public int UpdateTodoInfo(Todo todo)
        {
            int res = TodoRepository.UpdateTodo(todo);
            return res;
        }

        public int DeleteTodoInfo(Todo todo)
        {
            int res = TodoRepository.DeleteTodo(todo);
            return res;
        }

        public DataTable QueryTodoInfo()
        {
            return TodoRepository.QueryTodo();
        }
    }
}
