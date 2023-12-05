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
using System.Data;
using UIDisplay.BLL;

namespace UIDisplay.Pages
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterPage : System.Windows.Window
    {
        private readonly string _newEmail;

        public RegisterPage()   //临时写的，用来测试
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // 导航回登录页面
            Login login = new Login(); // 创建登录页面实例
            login.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs first
            if (!ValidateInputs()) return;

            // Save user data to the database
            if (SaveUserDataToDatabase())
            {
                // Data saved successfully, navigate to the main page
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                // Data save failed, display an error message to the user
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
            var newUserName = txtNickname.Text;
            var newDateOfBirth = txtDateOfBirth.SelectedDate.Value;
            var newPassword = txtPassword?.Password;
            return UserManager.InsertUser(newUserName, newDateOfBirth, _newEmail, newPassword);
        }
    }
}
