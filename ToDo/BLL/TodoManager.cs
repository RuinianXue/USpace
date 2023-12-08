using System.Collections.Generic;
using System;
using System.Data;
using System.Text.RegularExpressions;
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

        public static async Task<DataTable> QueryTodoAsync(string userID)
        {
            return await TodoRepository.QueryTodoAsync(userID);
        }

        public static (DateTime? ParsedDateTime, string Content) ParseTime(string input)
        {
            DateTime now = DateTime.Now;
            DayOfWeek currentDayOfWeek = now.DayOfWeek;
            string pattern = @"(?i)(\b明天|\b后天|\b大后天)?(\b本周|\b下周)?([一二三四五六日周])?((早上|上午|下午|晚上)?(\d+)点(\d+)?(分(钟)?)?)?([\u4E00-\u9FFF]+)?";
            Match match = Regex.Match(input, pattern);
            string Content = string.Empty;

            if (match.Success)
            {
                string dayModifier = match.Groups[1].Value;
                string weekModifier = match.Groups[2].Value;
                string dayOfWeekPhrase = match.Groups[3].Value;
                string timePhrase = match.Groups[4].Value;
                string hour = match.Groups[6].Value;
                string minute = match.Groups[7].Value;  //?

                for (int i = 0; i < match.Groups.Count; i++)
                {
                    if ((match.Groups[i].Value).Length >= 1)
                        Content = match.Groups[i].Value;
                }

                int daysToAdd = 0;
                if (!string.IsNullOrEmpty(dayModifier) && dayModifiers.ContainsKey(dayModifier))
                {
                    daysToAdd = dayModifiers[dayModifier];
                }

                DayOfWeek targetDayOfWeek = currentDayOfWeek;
                if (!string.IsNullOrEmpty(weekModifier) && dayOfWeekPhrases.ContainsKey(weekModifier + dayOfWeekPhrase))
                {
                    targetDayOfWeek = dayOfWeekPhrases[weekModifier + dayOfWeekPhrase];
                }
                else if (!string.IsNullOrEmpty(dayOfWeekPhrase))
                {
                    targetDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeekPhrase, true);
                }

                int hours = now.Hour;
                if (!string.IsNullOrEmpty(hour))
                {
                    hours = int.Parse(hour);
                }
                if (timePhrase == "下午" || timePhrase == "晚上")
                {
                    hours += 12;
                }

                int minutes = 0;
                if (!string.IsNullOrEmpty(minute))
                {
                    minutes = int.Parse(minute);
                }
                DateTime dt = GetNextDateTime(now, targetDayOfWeek, daysToAdd).Date.AddHours(hours).AddMinutes(minutes);
                return (dt, Content);
            }
            match = Regex.Match(input, @"(\d+)(个)?(半)?((小时)?|(分钟)?|(秒钟)?)(\d+)?((小时)?|(分钟)?|(秒(钟)?)?)后([\u4E00-\u9FFF]+)?");
            if (match.Success)
            {
                int value = int.Parse(match.Groups[1].Value);
                string unit = match.Groups[4].Value;
                int value2 = 0;
                if (!string.IsNullOrEmpty(match.Groups[8].Value))
                    value2 = int.Parse(match.Groups[8].Value);
                string unit2 = match.Groups[9].Value;

                for (int i = 0; i < match.Groups.Count; i++)
                {
                    if ((match.Groups[i].Value).Length >= 2)
                        Content = match.Groups[i].Value;
                }

                DateTime dt = DateTime.Now;
                if (unit == "小时")
                {
                    if (input.Contains("半"))
                    {
                        dt = dt.AddHours(value).AddMinutes(30);
                    }
                    else
                    {
                        dt = dt.AddHours(value);
                    }
                }

                if (unit == "分钟")
                    dt = dt.AddMinutes(value);

                if (unit == "秒钟")
                    dt = dt.AddSeconds(value);

                if (!string.IsNullOrEmpty(unit2))
                {
                    if (unit2 == "分钟")
                    {
                        if (value2 != 0)
                            dt = dt.AddMinutes(value2);
                        else dt = dt.AddMinutes(value);
                    }
                    if (unit2 == "秒钟")
                    {
                        if (value2 != 0)
                            dt = dt.AddSeconds(value2);
                        else dt = dt.AddSeconds(value);
                    }
                }
                return (dt, Content);
            }

            return (null, input);
        }

        private static DateTime GetNextDateTime(DateTime now, DayOfWeek targetDayOfWeek, int daysToAdd)
        {
            int currentDayOfWeek = (int)now.DayOfWeek;
            int targetDayOfWeekValue = (int)targetDayOfWeek;
            int daysToTargetDay = (targetDayOfWeekValue - currentDayOfWeek + 7) % 7;
            return now.AddDays(daysToAdd + daysToTargetDay);
        }

        static readonly Dictionary<string, int> dayModifiers = new Dictionary<string, int>
        {
            { "今天", 0 },
            { "明天", 1 },
            { "后天", 2 },
            { "大后天", 3 }
        };

        static readonly Dictionary<string, DayOfWeek> dayOfWeekPhrases = new Dictionary<string, DayOfWeek>
        {
            { "本周一", DayOfWeek.Monday },
            { "本周二", DayOfWeek.Tuesday },
            { "本周三", DayOfWeek.Wednesday },
            { "本周四", DayOfWeek.Thursday },
            { "本周五", DayOfWeek.Friday },
            { "本周六", DayOfWeek.Saturday },
            { "本周日", DayOfWeek.Sunday },
            { "下周一", DayOfWeek.Monday },
            { "下周二", DayOfWeek.Tuesday },
            { "下周三", DayOfWeek.Wednesday },
            { "下周四", DayOfWeek.Thursday },
            { "下周五", DayOfWeek.Friday },
            { "下周六", DayOfWeek.Saturday },
            { "下周日", DayOfWeek.Sunday }
        };
    }
}
