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
        public string TID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }
        public int IsDone { get; set; }
        public string Teammate { get; set; }
        public string UID { get; set; }

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

        public int CompareTo(Todo other)
        {
            if (Priority!=other.Priority)
            {
                return Priority.CompareTo(other.Priority);
            }
            return other.Date.CompareTo(Date);
        }
    }
}
