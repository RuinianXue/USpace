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
    /// 代办事项页面，用于显示和管理用户的代办事项。
    /// </summary>
    public partial class TodoListPage : Page
    {
        private CancellationTokenSource cancellationTokenSource;
        private const int RefreshInterval = 60000; // 毫秒

        /// <summary>
        /// 初始化 <see cref="TodoListPage"/> 类的新实例。
        /// </summary>
        public TodoListPage()
        {
            InitializeComponent();
            TodoListPageInitialize();
        }

        /// <summary>
        /// 初始化 TodoList 页面。
        /// </summary>
        private void TodoListPageInitialize()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => CheckTimeAsync(cancellationTokenSource.Token));
        }

        /// <summary>
        /// 异步检查时间并发送通知。
        /// </summary>
        /// <param name="cancellationToken">用于取消异步操作的标记。</param>
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

        /// <summary>
        /// 检查并发送通知。
        /// </summary>
        /// <param name="children">子元素集合。</param>
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

        /// <summary>
        /// 停止 TodoList 页面的异步操作。
        /// </summary>
        private void StopTodoListPage()
        {
            cancellationTokenSource?.Cancel();
        }

        /// <summary>
        /// 发送通知。
        /// </summary>
        /// <param name="todo">待办事项对象。</param>
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

        /// <summary>
        /// 刷新地址簿。
        /// </summary>
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
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    wrapPanel.Children.Clear();
                    Growl.Info("联系人列表为空！");
                }));
            }
        }

        /// <summary>
        /// 刷新代办事项列表。
        /// </summary>
        private void Refresh_TodoList()
        {
            todoList.Refresh();
        }

        /// <summary>
        /// 页面加载事件。
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInAnimation(sender);
            Refresh_TodoList();
            Refresh_Addressbook();
        }

        /// <summary>
        /// 页面卸载事件。
        /// </summary>
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            StopTodoListPage();
        }

        /// <summary>
        /// 加载页面动画。
        /// </summary>
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

        /// <summary>
        /// 任务内容文本框内容变化事件。
        /// </summary>
        private void TextBox_TodoTaskContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (todoTaskContentTextBox.Text.Length > 0)
            {
                spFuncArea.Visibility = Visibility.Visible;
                SetSelectedDateTimeFromText(todoTaskContentTextBox.Text);
            }
            else
            {
                spFuncArea.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 任务内容文本框获得焦点事件。
        /// </summary>
        private void TextBox_TodoTaskContent_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "focus");
        }

        /// <summary>
        /// 页面边框鼠标按下事件。
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("border: " + "mouse_down");
            if (g0Focus0.Visibility == Visibility.Visible)
            {
                g0Focus0.Visibility = Visibility.Collapsed;
                g0Focus1.Visibility = Visibility.Visible;
                g1Focus0.Visibility = Visibility.Collapsed;
                g1Focus1.Visibility = Visibility.Visible;
                todoTaskContentTextBox.Focus();
            }
        }

        /// <summary>
        /// 任务内容文本框失去焦点事件。
        /// </summary>
        private void TextBox_TodoTaskContent_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("todoTaskContentTextBox: " + "lost_focus");
        }

        /// <summary>
        /// 任务内容文本框失去焦点事件。
        /// </summary>
        private void TextBox_TodoTaskContent_LostFocus()
        {
            todoTaskContentTextBox.Text = null;
            g0Focus0.Visibility = Visibility.Visible;
            g0Focus1.Visibility = Visibility.Collapsed;
            g1Focus0.Visibility = Visibility.Visible;
            g1Focus1.Visibility = Visibility.Collapsed;
            teammateList.Text = "无";
        }

        /// <summary>
        /// 从文本中设置选定的日期时间。
        /// </summary>
        private void SetSelectedDateTimeFromText(string text)
        {
            var result = TodoManager.ParseTime(text);
            DateTime? dt = result.ParsedDateTime;
            if (dt != null)
            {
                dateTimePickers.SelectedDateTime = dt;
            }
            else
            {
                dateTimePickers.SelectedDateTime = DateTime.Now.AddDays(1);
            }
        }

        /// <summary>
        /// 从文本中获取内容。
        /// </summary>
        private string GetContentFromText(string text)
        {
            var result = TodoManager.ParseTime(text);
            string dt = result.Content;
            return dt;
        }

        /// <summary>
        /// 页面键盘按下事件。
        /// </summary>
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && g1Focus1.Visibility == Visibility.Visible)
            {
                string text = todoTaskContentTextBox.Text;
                if (text.Length > 0)
                {
                    string teammate = (teammateList.Text == "无") ? null : teammateList.Text;

                    if (GetContentFromText(text) != null)
                    {
                        text = GetContentFromText(text);
                    }

                    Todo tmp_todoInfo = new Todo(IDGenerator.GenUUID(), text, dateTimePickers.SelectedDateTime.Value, 0, 0, teammateList.Text, LoginManager.CurrentUserID);

                    Task.Run(() =>
                    {
                        todoList.InsertTodo(tmp_todoInfo);
                    });
                    Task.Run(() =>
                    {
                        Dispatcher.BeginInvoke(new Action(delegate
                        {
                            TodoUnit todoUnit = new TodoUnit(todoList, tmp_todoInfo);
                            todoUnit.AddTodoUnitIntoTodoList();
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

        /// <summary>
        /// 切换地址簿可见性。
        /// </summary>
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

        /// <summary>
        /// 点击添加联系人按钮事件。
        /// </summary>
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

        /// <summary>
        /// 点击地址簿刷新按钮事件。
        /// </summary>
        private void Btn_AddressbookRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Addressbook();
        }

        /// <summary>
        /// 确认联系人列表按钮点击事件。
        /// </summary>
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
