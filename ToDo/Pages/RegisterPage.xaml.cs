using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HandyControl.Controls;
using UIDisplay.BLL;
using UIDisplay.Model;
using System.IO;
using UIDisplay.Utils;
using Newtonsoft.Json;

namespace UIDisplay.Pages
{
    /// <summary>
    /// 注册页面，用于用户注册操作。
    /// </summary>
    public partial class RegisterPage : System.Windows.Window
    {
        // 存储新注册用户的邮箱
        private readonly string _newEmail;

        /// <summary>
        /// 初始化 <see cref="RegisterPage"/> 类的新实例。
        /// </summary>
        /// <param name="newEmail">新注册用户的邮箱。</param>
        public RegisterPage(string newEmail)
        {
            InitializeComponent();
            _newEmail = newEmail;
        }

        /// <summary>
        /// 鼠标按下事件，用于实现窗口拖动。
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        /// <summary>
        /// 取消按钮点击事件，用于返回登录页面。
        /// </summary>
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            LoginPage login = new LoginPage();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// 保存按钮点击事件，用于验证输入并保存用户数据到数据库。
        /// </summary>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            if (SaveUserDataToDatabase())
            {
                Growl.Success("注册用户成功");
                PerformLogin();
            }
            else
            {
                Growl.Error("注册用户失败！");
            }
        }

        /// <summary>
        /// 验证用户输入的有效性。
        /// </summary>
        /// <returns>如果输入有效则返回 true，否则返回 false。</returns>
        private bool ValidateInputs()
        {
            var newPassword = txtPassword?.Password;
            var newConfirmPassword = txtConfirmPassword?.Password;

            if (string.IsNullOrEmpty(txtNickname.Text))
            {
                Growl.Error("请输入昵称！");
                return false;
            }

            if (txtDateOfBirth.SelectedDate == null)
            {
                Growl.Error("请输入您的生日！");
                return false;
            }

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newConfirmPassword))
            {
                Growl.Error("请输入密码！");
                return false;
            }

            if (newPassword != newConfirmPassword)
            {
                Growl.Error("两次输入密码不一致！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 将用户数据保存到数据库。
        /// </summary>
        /// <returns>如果保存成功则返回 true，否则返回 false。</returns>
        private bool SaveUserDataToDatabase()
        {
            var newUID = IDGenerator.GenUUID();
            var newUserName = txtNickname.Text;
            var newDateOfBirth = txtDateOfBirth.SelectedDate.Value;
            var newPassword = txtPassword?.Password;
            var newJsonFilePath = $"../../../AppData/{newUID}.json";

            User newUser = new User
            {
                UID = newUID,
                Nickname = newUserName,
                DateOfBirth = newDateOfBirth,
                Email = _newEmail,
                Password = newPassword,
                JsonFilePath = newJsonFilePath
            };

            bool insertSuccess = UserManager.InsertUser(newUser);

            if (insertSuccess)
            {
                CreateJson(newJsonFilePath);
            }

            return insertSuccess;
        }

        /// <summary>
        /// 创建 JSON 文件。
        /// </summary>
        private void CreateJson(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, "");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating JSON file: {ex.Message}");
                Growl.Error("生成Json文件失败！");
            }
        }

        /// <summary>
        /// 执行登录操作。
        /// </summary>
        private void PerformLogin()
        {
            bool success = LoginManager.PerformLogin(_newEmail);
            if (success)
            {
                Growl.Success("登录成功");
                NavigateToMainPage();
            }
            else
            {
                Growl.Error("登录失败！请稍后再试。");
            }
        }

        /// <summary>
        /// 跳转至主页面。
        /// </summary>
        private void NavigateToMainPage()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
