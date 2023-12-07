using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HandyControl.Controls;
using UIDisplay.BLL;
using UIDisplay.Model;
using System.IO;
using Newtonsoft.Json;

namespace UIDisplay.Pages
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterPage : System.Windows.Window
    {
        private readonly string _newEmail;

        public RegisterPage()   //待修改：临时写的，用来测试
        {
            InitializeComponent();
            _newEmail = "2857809611@qq.com";
        }

        public RegisterPage(string newEmail)
        {
            InitializeComponent();
            _newEmail = newEmail;
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            LoginPage login = new LoginPage(); 
            login.Show();
            this.Close();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            if (SaveUserDataToDatabase())
            {
                PerformLogin();
            }
            else
            {
                Growl.Error("Failed to save user data. Please try again.");
            }
        }

        private bool ValidateInputs()
        {
            var newPassword = txtPassword?.Password;
            var newConfirmPassword = txtConfirmPassword?.Password;

            if(string.IsNullOrEmpty(txtNickname.Text))
            {
                Growl.Error("Nickname cannot be empty");
                return false;
            }

            if (txtDateOfBirth.SelectedDate == null)
            {
                Growl.Error("Please select a valid date of birth");
                return false;
            }

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newConfirmPassword))
            {
                Growl.Error("Password cannot be empty");
                return false;
            }

            if (newPassword != newConfirmPassword)
            {
                Growl.Error("Password and Confirm Password do not match");
                return false;
            }

            return true;
        }

        private bool SaveUserDataToDatabase()
        {
            var newUID = IDManager.genUUID();
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
                //JsonFilePath = $"D:\\0temp\\USpace\\ToDo\\dashboard.json"
                JsonFilePath = newJsonFilePath
            };

            bool insertSuccess = UserManager.InsertUser(newUser);

            if (insertSuccess)
            {
                CreateJson(newJsonFilePath);
            }

            return insertSuccess;
        }

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

        private void NavigateToMainPage()
        {
            // 创建主页面实例
            MainWindow mainWindow = new MainWindow();

            // 导航到主页面
            mainWindow.Show();

            this.Close();
        }
    }
}
