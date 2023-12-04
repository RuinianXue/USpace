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
    public partial class Register : System.Windows.Window
    {
        public Register()
        {
            InitializeComponent();
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
            //this.NavigationService.Navigate(loginPage); // 使用导航服务导航到登录页面
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = txtPassword.Password; // 获取用户输入的密码
            string confirmPassword = txtConfirmPassword.Password; // 获取用户输入的确认密码
            Growl.Info("Enter:" + newPassword + ", Again: " + confirmPassword);
            // 保存用户注册信息到数据库
            bool saveSuccess = SaveUserDataToDatabase();

            if (saveSuccess)
            {
                // 数据保存成功，导航到主页面
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // 数据保存失败，可以进行相应的错误处理，例如显示错误消息给用户
                Growl.Error("Failed to save user data. Please try again.");
            }
        }

        private bool SaveUserDataToDatabase()
        {
            string newPassword = txtPassword.Password; // 获取用户输入的密码
            string confirmPassword = txtConfirmPassword.Password; // 获取用户输入的确认密码
            Growl.Info("Enter:" + newPassword + ", Again: " + confirmPassword);
            if (newPassword != confirmPassword)
            {
                // 显示密码不匹配的错误消息
                Growl.Error("Password and Confirm Password do not match");
                return false; // 返回false表示保存操作失败
            }

            // 从界面获取用户输入的用户名、出生日期、邮箱和密码
            string newUserName = txtNickname.Text;
            DateTime newDateOfBirth = DateTime.Now;
            string newEmail = "2857809611@qq.com";  //待修改!!!!!!!!!!

            bool flag = UserManager.InsertUser(newUserName, newDateOfBirth, newEmail, newPassword);


            return flag;
            //return UserManager.InsertUser(newUserName, newDateOfBirth, newEmail, newPassword);
        }
    }
}
