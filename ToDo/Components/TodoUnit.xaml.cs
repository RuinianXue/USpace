using UIDisplay.Utils;
using UIDisplay.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIDisplay.Model;

namespace UIDisplay.Components
{
    /// <summary>
    /// 表示一个待办事项单元的用户界面组件。
    /// </summary>
    public partial class TodoUnit : UserControl
    {
        public Todo todo;
        TodoList todoList;

        /// <summary>
        /// 初始化 <see cref="TodoUnit"/> 类的新实例。
        /// </summary>
        public TodoUnit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化 <see cref="TodoUnit"/> 类的新实例，并指定所属的待办事项列表和待办事项信息。
        /// </summary>
        /// <param name="todolist">所属的待办事项列表。</param>
        /// <param name="todoInfo">待办事项信息。</param>
        public TodoUnit(TodoList todolist, Todo todoInfo)
        {
            InitializeComponent();
            this.todo = todoInfo;
            this.todoList = todolist;
            Init();
        }

        private void Init()
        {
            todoContentText.Text = todo.Content;
            todoDateTimeText.Text = todo.Date.ToString("MM-dd HH:mm");
            todoTeammateListText.Text = todo.Teammate;
            if (todo.IsDone > 0)
            {
                Btn_IsDone.IsChecked = true;
            }
            if (todo.Priority > 0)
            {
                isImportantBtn.IsChecked = true;
            }
        }

        private void Panel_UnCheck_MouseEnter(object sender, MouseEventArgs e)
        {
            checkHoverShow.Visibility = Visibility.Visible;
        }

        private void Panel_UnCheck_MouseLeave(object sender, MouseEventArgs e)
        {
            checkHoverShow.Visibility = Visibility.Hidden;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6)
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 200,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.6),
                DecelerationRatio = 1
            };
            Storyboard.SetTarget(doubleAnimation, mainBorder);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, mainBorder);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
        }

        private void Border_Star_MouseEnter(object sender, MouseEventArgs e)
        {
            starPath.Fill = (SolidColorBrush)this.FindResource("PrimaryBlueColor");
        }

        private void Border_Star_MouseLeave(object sender, MouseEventArgs e)
        {
            starPath.Fill = (SolidColorBrush)this.FindResource("PrimaryGrayColor");
        }

        private void Btn_IsDone_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Btn_IsDone: " + Btn_IsDone.IsChecked);
            AnimateFadeOutAndSlideLeft(mainBorder);

            todo.IsDone = Btn_IsDone.IsChecked == true ? 1 : 0;
            todoList.UpdateTodo(todo);
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {

                    if (Btn_IsDone.IsChecked == false)
                    {
                        todoList.todoList2.Children.Remove(this);
                    }
                    else if (isImportantBtn.IsChecked == true)
                    {
                        todoList.todoList0.Children.Remove(this);
                    }
                    else
                    {
                        todoList.todoList1.Children.Remove(this);
                    }
                    AddTodoUnitIntoTodoList();
                }));
            });
        }

        private void AnimateFadeOutAndSlideLeft(UIElement element)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.8)
            };
            Storyboard.SetTarget(opacityAnimation, element);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(opacityAnimation);

            DoubleAnimation slideLeftAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(0.8),
                DecelerationRatio = 1
            };
            Storyboard.SetTarget(slideLeftAnimation, element);
            Storyboard.SetTargetProperty(slideLeftAnimation, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            storyboard.Children.Add(slideLeftAnimation);

            storyboard.Begin();
        }


        private void Btn_IsImportant_Click(object sender, RoutedEventArgs e)
        {
            todo.Priority = isImportantBtn.IsChecked == true ? 5 : 0;
            todoList.UpdateTodo(todo);
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    if (Btn_IsDone.IsChecked == true)
                    {
                        todoList.todoList2.Children.Remove(this);
                    }
                    else if (isImportantBtn.IsChecked == true)
                    {
                        todoList.todoList1.Children.Remove(this);
                    }
                    else
                    {
                        todoList.todoList0.Children.Remove(this);
                    }
                    AddTodoUnitIntoTodoList();
                }));
            });
        }

        public void AddTodoUnitIntoTodoList()
        {
            if (Btn_IsDone.IsChecked == true)
            {
                todoList.todoList2.Children.Insert(0, this);
            }
            else if (isImportantBtn.IsChecked == true)
            {
                int pos = todoList.todoList0.Children.Count;
                // 二分查找第一个小于this的TodoUnit
                int l = 0, r = pos - 1;
                while (l <= r)
                {
                    int mid = (l + r) >> 1;
                    if (todo.CompareTo(((TodoUnit)todoList.todoList0.Children[mid]).todo) >= 0)
                    {
                        pos = mid;
                        r = mid - 1;
                    }
                    else
                    {
                        l = mid + 1;
                    }
                }
                todoList.todoList0.Children.Insert(pos, this);
            }
            else
            {
                int pos = todoList.todoList1.Children.Count;
                for (int i = 0; i < todoList.todoList1.Children.Count; i++)
                {
                    if (todo.CompareTo(((TodoUnit)todoList.todoList1.Children[i]).todo) >= 0)
                    {
                        pos = i;
                        break;
                    }
                }
                todoList.todoList1.Children.Insert(pos, this);
            }
        }
        private void Btn_IsDone_Checked(object sender, RoutedEventArgs e)
        {
            todoContentText.Opacity = 0.7;
            todoContentText.TextDecorations = TextDecorations.Strikethrough;
            todoTeammateListTextTitle.Opacity = 0.8;
            todoTeammateListText.Opacity = 0.8;
            calenderIcon.Fill = (SolidColorBrush)this.FindResource("TextPrimaryColor");
            calenderIcon.Opacity = 0.7;
            todoDateTimeText.Foreground = (SolidColorBrush)this.FindResource("TextPrimaryColor");
            todoDateTimeText.Opacity = 0.7;
        }

        private void Btn_IsDone_Unchecked(object sender, RoutedEventArgs e)
        {
            todoContentText.Opacity = 1;
            todoContentText.TextDecorations = null;
            todoTeammateListTextTitle.Opacity = 1;
            todoTeammateListText.Opacity = 1;
            calenderIcon.Fill = (LinearGradientBrush)this.FindResource("DangerBrush");
            calenderIcon.Opacity = 1;
            todoDateTimeText.Foreground = (LinearGradientBrush)this.FindResource("DangerBrush");
            todoDateTimeText.Opacity = 1;
        }

        private void Border_Main_MouseEnter(object sender, MouseEventArgs e)
        {
            mainBorder.Background = (SolidColorBrush)this.FindResource("TEAL_A");
        }

        private void Border_Main_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBorder.Background = (SolidColorBrush)this.FindResource("PrimaryBackgroundColor");
        }

        private void MI_Delete_Click(object sender, RoutedEventArgs e)
        {
            todoList.DeleteTodo(todo);
            Task.Run(() =>
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {

                    if (Btn_IsDone.IsChecked == true)
                    {
                        todoList.todoList2.Children.Remove(this);
                    }
                    else if (isImportantBtn.IsChecked == true)
                    {
                        todoList.todoList0.Children.Remove(this);
                    }
                    else
                    {
                        todoList.todoList1.Children.Remove(this);
                    }
                }));
            });
        }
    }
}
