﻿using HandyControl.Controls;
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
    /// 表示一个待办事项列表的用户界面组件。
    /// </summary>
    public partial class TodoList : UserControl
    {
        private bool IsShowMore { get; set; } = true;

        /// <summary>
        /// 初始化 <see cref="TodoList"/> 类的新实例。
        /// </summary>
        public TodoList()
        {
            InitializeComponent();
            TodoListInitialize();
        }

        private void TodoListInitialize()
        {
            Refresh();
        }

        /// <summary>
        /// 刷新待办事项列表。
        /// </summary>
        internal async void Refresh()
        {
            DataTable dt = await TodoManager.QueryTodoAsync(LoginManager.CurrentUserID);

            List<Todo> todoUnitList0 = ExtractTodoList(dt, 0, priority => priority > 0);
            List<Todo> todoUnitList1 = ExtractTodoList(dt, 0, priority => priority <= 0);
            List<Todo> todoUnitList2 = ExtractTodoList(dt, 1, priority => true);

            UpdateUI(todoUnitList0, todoList0);
            UpdateUI(todoUnitList1, todoList1);
            UpdateUI(todoUnitList2, todoList2);

            Refresh_TodoDoneCount();
        }

        private List<Todo> ExtractTodoList(DataTable dt, int isDoneValue, Func<int, bool> priorityCondition)
        {
            List<Todo> todoList = new List<Todo>();

            foreach (DataRow row in dt.Rows)
            {
                string tid = row.Field<string>(0);
                string content = row.Field<string>(1);
                DateTime date = row.Field<DateTime>(2);
                string uid = LoginManager.CurrentUserID;

                // 处理可能的 DBNull 值
                int priority = row.IsNull(3) ? 0 : row.Field<int>(3);
                int isDone = Convert.ToInt32(row.IsNull(4) ? 0 : row[4]);

                string teammate = row.Field<string>(5);

                if (isDone == isDoneValue && priorityCondition(priority))
                {
                    todoList.Add(new Todo(tid, content, date, priority, isDone, teammate, uid));
                }
            }

            return todoList;
        }

        private void UpdateUI(List<Todo> todoList, Panel targetPanel)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                targetPanel.Children.Clear();
                foreach (Todo subTodo in todoList)
                {
                    targetPanel.Children.Add(new TodoUnit(this, subTodo));
                }
            }));
        }

        private void Refresh_TodoDoneCount()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                todoDoneCount.Text = todoList2.Children.Count.ToString();
            }));
        }

        /// <summary>
        /// 更多按钮点击事件处理。
        /// </summary>
        private void Btn_More_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// 插入待办事项。
        /// </summary>
        /// <param name="todo">待插入的待办事项。</param>
        public void InsertTodo(Todo todo)
        {
            Task.Run(() =>
            {
                bool success = TodoManager.InsertTodo(todo);
                if (success)
                {
                    Growl.Success("待办任务新建成功！");
                }
                else
                {
                    Growl.Warning("待办任务新建失败！");
                }
            });
        }

        /// <summary>
        /// 更新待办事项。
        /// </summary>
        /// <param name="todo">待更新的待办事项。</param>
        public void UpdateTodo(Todo todo)
        {
            Task.Run(() =>
            {
                bool success = TodoManager.UpdateTodo(todo);
                if (success)
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

        /// <summary>
        /// 删除待办事项。
        /// </summary>
        /// <param name="todo">待删除的待办事项。</param>
        public void DeleteTodo(Todo todo)
        {
            Task.Run(() =>
            {
                bool success = TodoManager.DeleteTodo(todo);
                if (success)
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
