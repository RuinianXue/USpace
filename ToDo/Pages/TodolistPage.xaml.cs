﻿using HandyControl.Controls;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDisplay.Pages
{
    /// <summary>
    /// TodolistPage.xaml 的交互逻辑
    /// </summary>
    public partial class TodolistPage : Page
    {
        private TodoDataControl todoDataControl;

        private bool isShowMore { get; set; } = true;
        public TodolistPage()
        {
            InitializeComponent();
            TodoListPageInitialize();

        }
        private void TodoListPageInitialize()
        {
            //this.Width = Constants.INSIDE_WIDTH;
            //this.Height = Constants.INSIDE_HEIGHT;
            Refresh();
            Task.Run(checkTime);
            addressbookRefresh();
        }
        private void checkTime()
        {
            while (true)
            {
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    foreach (TodoUnit todoUnit in todoList0.Children)
                    {
                        if (DateTime.Now.AddMinutes(1) < todoUnit.todoInfo.Date)
                        {
                            break;
                        }
                        else if (todoUnit.todoInfo.Date >= DateTime.Now)
                        {
                            string[] emailList = todoUnit.todoInfo.Teammate.Split(';');
                            foreach (string email in emailList)
                            {
                                Console.WriteLine(email);
                                //MyEmail.SendEmail(email, "您有一个任务有待完成", todoUnit.todoInfo.Content);
                            }

                        }
                    }
                    foreach (TodoUnit todoUnit in todoList1.Children)
                    {
                        if (DateTime.Now.AddMinutes(1) < todoUnit.todoInfo.Date)
                        {
                            break;
                        }
                        else if (todoUnit.todoInfo.Date >= DateTime.Now)
                        {
                            string[] emailList = todoUnit.todoInfo.Teammate.Split(';');
                            foreach (string email in emailList)
                            {
                                Console.WriteLine(email);
                                //MyEmail.SendEmail(email, "您有一个任务有待完成", todoUnit.todoInfo.Content);
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
                todoDataControl = new TodoDataControl();
                List<TodoInfo> todoUnitList0, todoUnitList1, todoUnitList2;
                todoUnitList0 = new List<TodoInfo>();
                todoUnitList1 = new List<TodoInfo>();
                todoUnitList2 = new List<TodoInfo>();
                DataTable dt = todoDataControl.queryTodoInfo();
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
                        todoUnitList0.Add(new TodoInfo(uuid, content, date, priority, isdone, teammate));
                    }
                    else if (isdone == 0)
                    {
                        todoUnitList1.Add(new TodoInfo(uuid, content, date, priority, isdone, teammate));
                    }
                    else
                    {
                        todoUnitList2.Add(new TodoInfo(uuid, content, date, priority, isdone, teammate));
                    }
                }
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    todoList0.Children.Clear();
                    foreach (TodoInfo sub_todoInfo in todoUnitList0)
                    {
                        todoList0.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList1.Children.Clear();
                    foreach (TodoInfo sub_todoInfo in todoUnitList1)
                    {
                        todoList1.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    todoList2.Children.Clear();
                    foreach (TodoInfo sub_todoInfo in todoUnitList2)
                    {
                        todoList2.Children.Add(new TodoUnit(this, sub_todoInfo));
                    }
                    Refresh_TodoDoneCount();
                }));
            });
        }
        private void addressbookRefresh()
        {
            Task.Run(() =>
            {
                //UserDataControl userDataControl = new UserDataControl();
                //DataTable dt = userDataControl.queryUserInfo();
                //Dispatcher.BeginInvoke(new Action(delegate
                //{
                //    wrapPanel.Children.Clear();
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        DataRow row = dt.Rows[i];
                //        UserInfo userInfo = new UserInfo(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                //        AddressUnit addressUnit = new AddressUnit(userInfo,1);
                //        wrapPanel.Children.Add(addressUnit);
                //    }
                //}));
            });

        }
        public void UpdateTodoInfo(TodoInfo todoInfo)
        {
            Task.Run(() =>
            {
                TodoDataControl tmp_todoDataControl = new TodoDataControl();
                tmp_todoDataControl.updateTodoInfo(todoInfo);
                Refresh_TodoDoneCount();

            });
        }
        public void DeleteTodoInfo(TodoInfo todoInfo)
        {
            Task.Run(() =>
            {
                TodoDataControl tmp_todoDataControl = new TodoDataControl();
                tmp_todoDataControl.deleteTodoInfo(todoInfo);
                Refresh_TodoDoneCount();
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
            isShowMore = !isShowMore;
            double from = isShowMore ? 0 : 90;
            double to = isShowMore ? 90 : 0;
            todoList2.Visibility = isShowMore ? Visibility.Visible : Visibility.Collapsed;
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

        private void todoTaskContentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if (todoTaskContentTextBox.Text.Length > 0)
            {
                //spFuncArea.Visibility = Visibility.Visible;
            }
            else
            {
                //spFuncArea.Visibility = Visibility.Collapsed;
            }*/
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
            //todoTaskContentTextBox.Text = null;
            //g0Focus0.Visibility = Visibility.Visible;
            //g0Focus1.Visibility = Visibility.Collapsed;
            //g1Focus0.Visibility = Visibility.Visible;
            //g1Focus1.Visibility = Visibility.Collapsed;
        }
        private void todoTaskContentTextBox_LostFocus()
        {
            todoTaskContentTextBox.Text = null;
            g0Focus0.Visibility = Visibility.Visible;
            g0Focus1.Visibility = Visibility.Collapsed;
            g1Focus0.Visibility = Visibility.Visible;
            g1Focus1.Visibility = Visibility.Collapsed;
            //teammateList.Text = "无";
        }

        private void todoTaskContentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Enter)
            {
                if (todoTaskContentTextBox.Text.Length > 0)
                {
                    TodoInfo tmp_todoInfo = new TodoInfo(MyUtils.genUUID(), todoTaskContentTextBox.Text, dateTimePickers.SelectedDateTime.Value, 0, 0, teammateList.Text);
                    Task.Run(() =>
                   {
                       todoDataControl = new TodoDataControl();
                       todoDataControl.insertTodoInfo(tmp_todoInfo);
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
            }*/
        }


        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void todolistPanelScr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            todoTaskContentTextBox_LostFocus();
        }

        private void Refresh_TodoDoneCount()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                todoDoneCount.Text = todoList2.Children.Count.ToString();
            }));
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && g1Focus1.Visibility == Visibility.Visible)
            {
                if (todoTaskContentTextBox.Text.Length > 0)
                {
                    //TodoInfo tmp_todoInfo = new TodoInfo(MyUtils.genUUID(), todoTaskContentTextBox.Text, dateTimePickers.SelectedDateTime.Value, 0, 0, teammateList.Text);

                    TodoInfo tmp_todoInfo = new TodoInfo(MyUtils.genUUID(), todoTaskContentTextBox.Text, dateTimePickers.SelectedDateTime.Value, 0, 0, 
                        "无");
                    string input = todoTaskContentTextBox.Text;

                    DateTime? parsedTime = tmp_todoInfo.ParseTime(input);

                    if (parsedTime != null)
                    {
                        tmp_todoInfo.Date = parsedTime.Value;
                    }

                    Task.Run(() =>
                    {
                        todoDataControl = new TodoDataControl();
                        todoDataControl.insertTodoInfo(tmp_todoInfo);
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
    }
}