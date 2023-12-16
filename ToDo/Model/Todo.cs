using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace UIDisplay.Model
{
    public class Todo : IComparable<Todo>
    {
        public string TID { get; set; } // 待办事项唯一标识符
        public string Content { get; set; } // 待办事项内容
        public DateTime Date { get; set; } // 待办事项日期
        public int Priority { get; set; } // 优先级
        public int IsDone { get; set; } // 是否已完成（0: 未完成，1: 已完成）
        public string Teammate { get; set; } // 关联的团队成员
        public string UID { get; set; } // 所属用户唯一标识符

        /// <summary>
        /// 构造函数，初始化Todo对象
        /// </summary>
        /// <param name="tID">待办事项唯一标识符</param>
        /// <param name="content">待办事项内容</param>
        /// <param name="date">待办事项日期</param>
        /// <param name="priority">优先级</param>
        /// <param name="isDone">是否已完成（0: 未完成，1: 已完成）</param>
        /// <param name="teammate">关联的团队成员</param>
        /// <param name="uID">所属用户唯一标识符</param>
        public Todo(string tID, string content, DateTime date, int priority, int isDone, string teammate, string uID)
        {
            TID = tID;
            Content = content;
            Date = date;
            Priority = priority;
            IsDone = isDone;
            Teammate = teammate;
            UID = uID;
        }

        /// <summary>
        /// 比较方法，用于排序
        /// </summary>
        /// <param name="other">要比较的另一个Todo对象</param>
        /// <returns>比较结果</returns>
        public int CompareTo(Todo other)
        {
            if (Priority != other.Priority)
            {
                return Priority.CompareTo(other.Priority);
            }
            return other.Date.CompareTo(Date);
        }
    }
}
