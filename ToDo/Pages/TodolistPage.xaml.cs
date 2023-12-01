using HandyControl.Controls;
using UIDisplay.Components;
using UIDisplay.Myscripts;
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

namespace UIDisplay.Pages
{
    /// <summary>
    /// TodolistPage.xaml 的交互逻辑
    /// </summary>
    public partial class TodoListPage : Page
    {
        private bool IsShowMore { get; set; } = true;
        public TodoListPage()
        {
            InitializeComponent();
            TodoListPageInitialize();
            Refresh_Addressbook();
        }
        private void TodoListPageInitialize()
        {
            //this.Width = Constants.INSIDE_WIDTH;
            //this.Height = Constants.INSIDE_HEIGHT;
            Refresh();
            Task.Run(CheckTime);
            Refresh_Addressbook();
        }
        private void CheckTime()
        {
            while (true)
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    foreach (TodoUnit todoUnit in todoList.todoList0.Children)
                    {
                        if (DateTime.Now.AddMinutes(1) < todoUnit.todo.Date)
                        {
                            break;
                        }
                        else if (todoUnit.todo.Date >= DateTime.Now)
                        {
                            string[] emailList = todoUnit.todo.Teammate.Split(';');
                            foreach (string email in emailList)
                            {
                                Console.WriteLine(email);
                                EmailManager.SendNotice(email, "您有一个任务有待完成", todoUnit.todo.Content);
                            }
                        }
                    }
                    foreach (TodoUnit todoUnit in todoList.todoList1.Children)
                    {
                        if (DateTime.Now.AddMinutes(1) < todoUnit.todo.Date)
                        {
                            break;
                        }
                        else if (todoUnit.todo.Date >= DateTime.Now)
                        {
                            string[] emailList = todoUnit.todo.Teammate.Split(';');
                            foreach (string email in emailList)
                            {
                                Console.WriteLine(email);
                                EmailManager.SendNotice(email, "您有一个任务有待完成", todoUnit.todo.Content);
                            }
                        }
                    }
                }));
                Thread.Sleep(60000);
            }
        }
        private void Refresh()
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
                    todoList.todoList0.Children.Clear();
                    foreach (Todo sub_todoInfo in todoUnitList0)
                    {
                        todoList.todoList0.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList.todoList1.Children.Clear();
                    foreach (Todo sub_todoInfo in todoUnitList1)
                    {
                        todoList.todoList1.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList.todoList2.Children.Clear();
                    foreach (Todo sub_todoInfo in todoUnitList2)
                    {
                        todoList.todoList2.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    Refresh_TodoDoneCount();
                }));
            });
        }

        private void Refresh_TodoDoneCount()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                todoList.todoDoneCount.Text = todoList.todoList2.Children.Count.ToString();
            }));
        }

        private void Refresh_Addressbook()
        {
            Task.Run(() =>
            {
                ContactManager userDataControl = new ContactManager();
                DataTable dt = userDataControl.QueryUserInfo();
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    wrapPanel.Children.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        Contact userInfo = new Contact(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                        AddressUnit addressUnit = new AddressUnit(userInfo, 1);
                        wrapPanel.Children.Add(addressUnit);
                    }
                }));
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
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
            todoList.todoList2.Visibility = IsShowMore ? Visibility.Visible : Visibility.Collapsed;
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(0.3),
                DecelerationRatio = 0.5
            };
            Storyboard.SetTarget(doubleAnimation, todoList.moreIcon);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.(RotateTransform.Angle)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

        private void todoTaskContentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (todoTaskContentTextBox.Text.Length > 0)
            {
                spFuncArea.Visibility = Visibility.Visible;
            }
            else
            {
                spFuncArea.Visibility = Visibility.Collapsed;
            }
        }

        private void todoTaskContentTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "focus");

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("border: " + "mouse_down");
            if (g0Focus0.Visibility == Visibility.Visible)
            {
                g0Focus0.Visibility = Visibility.Collapsed;
                g0Focus1.Visibility = Visibility.Visible;
                g1Focus0.Visibility = Visibility.Collapsed;
                g1Focus1.Visibility = Visibility.Visible;
                dateTimePickers.SelectedDateTime = DateTime.Now.AddDays(1);
                todoTaskContentTextBox.Focus();
            }
        }

        private void todoTaskContentTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "lost_focus");
        }

        private void todoTaskContentTextBox_LostFocus()
        {
            todoTaskContentTextBox.Text = null;
            g0Focus0.Visibility = Visibility.Visible;
            g0Focus1.Visibility = Visibility.Collapsed;
            g1Focus0.Visibility = Visibility.Visible;
            g1Focus1.Visibility = Visibility.Collapsed;
            teammateList.Text = "无";
        }

        //private void todolistPanelScr_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    todoTaskContentTextBox_LostFocus();
        //}

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && g1Focus1.Visibility == Visibility.Visible)
            {
                if (todoTaskContentTextBox.Text.Length > 0)
                {
                    Todo tmp_todoInfo = new Todo(MyUtils.genUUID(), todoTaskContentTextBox.Text, dateTimePickers.SelectedDateTime.Value, 0, 0, teammateList.Text);

                    Task.Run(() =>
                    {
                        InsertTodoInfo(tmp_todoInfo);
                    });
                    Task.Run(() =>
                    {
                        Dispatcher.BeginInvoke(new Action(delegate
                        {
                            TodoUnit todoUnit = new TodoUnit(this, tmp_todoInfo);
                            todoUnit.addTodoUnitIntoTodoList();
                        }));

                    });
                    todoTaskContentTextBox_LostFocus();
                }
                else
                {
                    Growl.Info("未输入任务内容");
                }
            }
        }

        private void addEmaillistBtn_Click(object sender, RoutedEventArgs e)
        {
            if (addressbookBorder.Visibility == Visibility.Visible)
            {
                Storyboard storyboard = new Storyboard();
                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1),
                    DecelerationRatio = 0.6
                };
                DoubleAnimation doubleAnimation2 = new DoubleAnimation()
                {
                    From = 0,
                    To = 50,
                    Duration = TimeSpan.FromSeconds(1),
                    DecelerationRatio = 0.6
                };
                Storyboard.SetTarget(doubleAnimation, addressbookBorder);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
                storyboard.Children.Add(doubleAnimation);
                Storyboard.SetTarget(doubleAnimation2, addressbookBorder);
                Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
                storyboard.Children.Add(doubleAnimation2);
                storyboard.Begin();
                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        addressbookBorder.Visibility = Visibility.Collapsed;
                    }));
                    Refresh_Addressbook();
                });

            }
            else
            {
                addressbookBorder.Visibility = Visibility.Visible;
                Storyboard storyboard = new Storyboard();
                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 0,
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
                Storyboard.SetTarget(doubleAnimation, addressbookBorder);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
                storyboard.Children.Add(doubleAnimation);
                Storyboard.SetTarget(doubleAnimation2, addressbookBorder);
                Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
                storyboard.Children.Add(doubleAnimation2);
                storyboard.Begin();
            }

        }

        private void addressbookRefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Addressbook();
        }

        private async void emailListConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                string emailList = "";
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    foreach (AddressUnit addressUnit in wrapPanel.Children)
                    {
                        if (addressUnit.IsChecked == true)
                        {
                            emailList += addressUnit.emailLabel.Text + ";";
                        }
                    }
                    if (emailList.Length == 0) emailList = "无";
                    teammateList.Text = emailList;
                }));
            });
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                DecelerationRatio = 0.6
            };
            DoubleAnimation doubleAnimation2 = new DoubleAnimation()
            {
                From = 0,
                To = 50,
                Duration = TimeSpan.FromSeconds(1),
                DecelerationRatio = 0.6
            };
            Storyboard.SetTarget(doubleAnimation, addressbookBorder);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, addressbookBorder);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    addressbookBorder.Visibility = Visibility.Collapsed;
                }));
                Refresh_Addressbook();
            });
        }

        public void InsertTodoInfo(Todo todoInfo)
        {
            Task.Run(() =>
            {
                int result = TodoManager.InsertTodoInfo(todoInfo);
                if (result > 0)
                {
                    Growl.Success("待办任务新建成功！");
                }
                else
                {
                    Growl.Warning("待办任务新建失败！");
                }
            });
        }

        //public void UpdateTodoInfo(Todo todoInfo)
        //{
        //    Task.Run(() =>
        //    {
        //        int result = TodoManager.UpdateTodoInfo(todoInfo);
        //        if (result > 0)
        //        {
        //            Growl.Success("待办任务更新成功！");
        //        }
        //        else
        //        {
        //            Growl.Warning("待办任务更新失败！");
        //        }
        //        Refresh_TodoDoneCount();
        //    });
        //}

        //public void DeleteTodoInfo(Todo todoInfo)
        //{
        //    Task.Run(() =>
        //    {
        //        int result = TodoManager.DeleteTodoInfo(todoInfo);
        //        if (result > 0)
        //        {
        //            Growl.Success("待办任务删除成功！");
        //        }
        //        else
        //        {
        //            Growl.Warning("待办任务删除失败！");
        //        }
        //        Refresh_TodoDoneCount();
        //    });
        //}
    }
}
