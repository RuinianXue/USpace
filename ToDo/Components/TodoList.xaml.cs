using HandyControl.Controls;
using UIDisplay.Components;
using UIDisplay.Utils;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UIDisplay.BLL;
using UIDisplay.Model;

namespace UIDisplay.Components
{
    /// <summary>
    /// TodoList.xaml 的交互逻辑
    /// </summary>
    public partial class TodoList : UserControl
    {
        private bool IsShowMore { get; set; } = true;
        public TodoList()
        {
            InitializeComponent();
            TodoListInitialize();
        }
        private void TodoListInitialize()
        {
            Refresh();
        }

        public void Refresh()   //待修改：暂时改为public
        {
            Task.Run(() =>
            {
                List<Todo> todoUnitList0, todoUnitList1, todoUnitList2;
                todoUnitList0 = new List<Todo>();
                todoUnitList1 = new List<Todo>();
                todoUnitList2 = new List<Todo>();
                DataTable dt = TodoManager.QueryTodoInfo();
                foreach (DataRow row in dt.Rows)
                {
                    string uuid = Convert.ToString(row[0]);
                    string content = Convert.ToString(row[1]);
                    DateTime date = Convert.ToDateTime(row[2]);
                    int priority = Convert.ToInt32(row[3]);
                    int isdone = Convert.ToInt32(row[4]);
                    string teammate = Convert.ToString(row[5]);

                    if (isdone == 0 && priority > 0)
                    {
                        todoUnitList0.Add(new Todo(uuid, content, date, priority, isdone, teammate));
                    }
                    else if (isdone == 0)
                    {
                        todoUnitList1.Add(new Todo(uuid, content, date, priority, isdone, teammate));
                    }
                    else
                    {
                        todoUnitList2.Add(new Todo(uuid, content, date, priority, isdone, teammate));
                    }
                }
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    todoList0.Children.Clear();
                    foreach (Todo sub_todoInfo in todoUnitList0)
                    {
                        todoList0.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList1.Children.Clear();
                    foreach (Todo sub_todoInfo in todoUnitList1)
                    {
                        todoList1.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList2.Children.Clear();
                    foreach (Todo sub_todoInfo in todoUnitList2)
                    {
                        todoList2.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    Refresh_TodoDoneCount();
                }));
            });
        }

        private void Refresh_TodoDoneCount()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                todoDoneCount.Text = todoList2.Children.Count.ToString();
            }));
        }

        private void LoadInAnimation(object sender)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0.4,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6),
                DecelerationRatio = 0.6
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8),
                DecelerationRatio = 0.6
            };
            Storyboard.SetTarget(doubleAnimation, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, (Page)sender);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            IsShowMore = !IsShowMore;
            double from = IsShowMore ? 0 : 90;
            double to = IsShowMore ? 90 : 0;
            todoList2.Visibility = IsShowMore ? Visibility.Visible : Visibility.Collapsed;
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(0.3),
                DecelerationRatio = 0.5
            };
            Storyboard.SetTarget(doubleAnimation, moreIcon);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.(RotateTransform.Angle)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

        //public void InsertTodoInfo(Todo todoInfo)
        //{
        //    Task.Run(() =>
        //    {
        //        int result = TodoManager.InsertTodoInfo(todoInfo);
        //        if (result > 0)
        //        {
        //            Growl.Success("待办任务新建成功！");
        //        }
        //        else
        //        {
        //            Growl.Warning("待办任务新建失败！");
        //        }
        //    });
        //}

        public void UpdateTodoInfo(Todo todoInfo)
        {
            Task.Run(() =>
            {
                int result = TodoManager.UpdateTodoInfo(todoInfo);
                if (result > 0)
                {
                    Growl.Success("待办任务更新成功！");
                }
                else
                {
                    Growl.Warning("待办任务更新失败！");
                }
                Refresh_TodoDoneCount();
            });
        }

        public void DeleteTodoInfo(Todo todoInfo)
        {
            Task.Run(() =>
            {
                int result = TodoManager.DeleteTodoInfo(todoInfo);
                if (result > 0)
                {
                    Growl.Success("待办任务删除成功！");
                }
                else
                {
                    Growl.Warning("待办任务删除失败！");
                }
                Refresh_TodoDoneCount();
            });
        }
    }
}
