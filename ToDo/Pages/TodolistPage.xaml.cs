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

namespace UIDisplay.Pages
{
    /// <summary>
    /// TodolistPage.xaml 的交互逻辑
    /// </summary>
    public partial class TodoListPage : Page
    {
        private CancellationTokenSource cancellationTokenSource;
        private const int RefreshInterval = 60000; // 毫秒

        public TodoListPage()
        {
            InitializeComponent();
            TodoListPageInitialize();
        }

        private void TodoListPageInitialize()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => CheckTimeAsync(cancellationTokenSource.Token));
        }

        private async Task CheckTimeAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    CheckAndSendNotifications(todoList.todoList0.Children);
                    CheckAndSendNotifications(todoList.todoList1.Children);
                });

                await Task.Delay(RefreshInterval);
            }
        }

        private void CheckAndSendNotifications(UIElementCollection children)
        {
            foreach (TodoUnit todoUnit in children.Cast<TodoUnit>().Where(todoUnit => DateTime.Now < todoUnit.todo.Date))
            {
                break;
            }

            foreach (TodoUnit todoUnit in children.Cast<TodoUnit>().Where(todoUnit => DateTime.Now >= todoUnit.todo.Date))
            {
                SendNotifications(todoUnit.todo);
            }
        }

        private void StopTodoListPage()
        {
            cancellationTokenSource?.Cancel();
        }

        private void SendNotifications(Todo todo)
        {
            string[] nameList = todo.Teammate.Split(';');
            foreach (string name in nameList)
            {
                if (!string.IsNullOrWhiteSpace(name) && name != "无")
                {
                    if (ContactManager.GetEmailByName(name, out string email))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            EmailManager.SendNotice(email, "您有一个任务有待完成", todo.Content);
                        }
                        else
                        {
                            Growl.Error($"未找到关联人{name}的邮箱");
                        }
                    }
                    else
                    {
                        Growl.Error($"未找到关联人{name}的邮箱");
                    }
                }
            }
        }

        private void Refresh_Addressbook()
        {
            DataTable dt;
            bool success = ContactManager.QueryAllContacts(LoginManager.CurrentUserID, out dt);

            if (success)
            {
                Dispatcher.Invoke(() =>
                {
                    wrapPanel.Children.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        var contact = new Contact(
                            row[0].ToString(),
                            row[1].ToString(),
                            row[2].ToString(),
                            row[3].ToString(),
                            row[4].ToString(),
                            row[5].ToString()
                        );
                        wrapPanel.Children.Add(new AddressUnit(contact, 1));
                    }
                });
            }
            else
            {
                Console.WriteLine();
            }
        }

        private void Refresh_TodoList()
        {
            todoList.Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
            Refresh_TodoList();
            Refresh_Addressbook();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            StopTodoListPage();
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

        private void TextBox_TodoTaskContent_TextChanged(object sender, TextChangedEventArgs e)
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

        private void TextBox_TodoTaskContent_GotFocus(object sender, RoutedEventArgs e)
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

        private void TextBox_TodoTaskContent_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "lost_focus");
        }

        private void TextBox_TodoTaskContent_LostFocus()
        {
            todoTaskContentTextBox.Text = null;
            g0Focus0.Visibility = Visibility.Visible;
            g0Focus1.Visibility = Visibility.Collapsed;
            g1Focus0.Visibility = Visibility.Visible;
            g1Focus1.Visibility = Visibility.Collapsed;
            teammateList.Text = "无";
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && g1Focus1.Visibility == Visibility.Visible)
            {
                if (todoTaskContentTextBox.Text.Length > 0)
                {
                    string teammate = (teammateList.Text == "无") ? null : teammateList.Text;

                    Todo tmp_todoInfo = new Todo(IDManager.genUUID(), todoTaskContentTextBox.Text, dateTimePickers.SelectedDateTime.Value, 0, 0, teammateList.Text, LoginManager.CurrentUserID);

                    Task.Run(() =>
                    {
                        todoList.InsertTodo(tmp_todoInfo);
                    });
                    Task.Run(() =>
                    {
                        Dispatcher.BeginInvoke(new Action(delegate
                        {
                            TodoUnit todoUnit = new TodoUnit(todoList, tmp_todoInfo);
                            todoUnit.addTodoUnitIntoTodoList();
                        }));
                    });
                    TextBox_TodoTaskContent_LostFocus();
                }
                else
                {
                    Growl.Info("未输入任务内容");
                }
            }
        }

        private void ToggleAddressbookVisibility(bool isVisible)
        {
            var opacityFrom = isVisible ? 0 : 1;
            var opacityTo = isVisible ? 1 : 0;
            var translateXFrom = isVisible ? 50 : 0;
            var translateXTo = isVisible ? 0 : 50;
            var duration = isVisible ? TimeSpan.FromSeconds(0.6) : TimeSpan.FromSeconds(1);

            var storyboard = new Storyboard();
            var doubleAnimation = new DoubleAnimation
            {
                From = opacityFrom,
                To = opacityTo,
                Duration = duration,
                DecelerationRatio = 0.6
            };
            var doubleAnimation2 = new DoubleAnimation
            {
                From = translateXFrom,
                To = translateXTo,
                Duration = duration,
                DecelerationRatio = 0.6
            };
            Storyboard.SetTarget(doubleAnimation, addressbookBorder);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation2, addressbookBorder);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            storyboard.Children.Add(doubleAnimation2);
            storyboard.Begin();

            if (!isVisible)
            {
                Task.Run(() =>
                {
                    Thread.Sleep((int)duration.TotalMilliseconds);
                    Dispatcher.Invoke(() => addressbookBorder.Visibility = Visibility.Collapsed);
                    Refresh_Addressbook();
                });
            }
        }

        private void Btn_AddEmailList_Click(object sender, RoutedEventArgs e)
        {
            if (addressbookBorder.Visibility == Visibility.Visible)
            {
                ToggleAddressbookVisibility(false);
            }
            else
            {
                addressbookBorder.Visibility = Visibility.Visible;
                ToggleAddressbookVisibility(true);
            }
        }

        private void Btn_AddressbookRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Addressbook();
        }

        private async void Btn_ContactListConfirm_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                var teammateList = "";
                Dispatcher.Invoke(() =>
                {
                    foreach (AddressUnit addressUnit in wrapPanel.Children)
                    {
                        if (addressUnit.IsChecked == true)
                        {
                            teammateList += addressUnit.nameLabel.Text + ";";
                        }
                    }
                    if (teammateList.Length == 0) teammateList = "无";
                    this.teammateList.Text = teammateList;
                });
            });

            ToggleAddressbookVisibility(false);
        }
    }
}
