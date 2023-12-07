using HandyControl.Controls;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System;
using UIDisplay.BLL;
using System.Threading.Tasks;

namespace UIDisplay.Pages
{
    public partial class LoginPage : System.Windows.Window
    {
        string verificationCode = null;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void VerificationCodeBox_CodeChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(verificationCodeBox.Password) && verificationCodeBox.Password.Length > 0)
                textVerificationCode.Visibility = Visibility.Collapsed;
            else
                textVerificationCode.Visibility = Visibility.Visible;
        }

        private void TextBlock_Password_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void TextBlock_VerificationCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            verificationCodeBox.Focus();
        }

        private void Btn_SignInByPassword_Click(object sender, RoutedEventArgs e)
        {
            string input = passwordBox.Password;

            if (string.IsNullOrEmpty(input))
            {
                Growl.Error("请输入密码！");
                return;
            }

            string email = txtEmail.Text;
            string password = passwordBox.Password;

            if (LoginManager.VerifyPassword(email, password))
            {
                PerformLogin();
            }
            else
            {
                Growl.Error("请输入有效的邮箱或密码！");
            }
        }

        private void Btn_SignInByCode_Click(object sender, RoutedEventArgs e)
        {
            string input = verificationCodeBox.Password;
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(input))
            {
                Growl.Error("请输入验证码！");
                return;
            }

            bool isEmailRegistered = LoginManager.CheckIfEmailIsRegistered(email);

            if (input == verificationCode)
            {
                if (!isEmailRegistered)
                {
                    NavigateToRegistrationPage();
                }
                else
                {
                    PerformLogin();
                }
            }
            else
            {
                Growl.Error("请输入有效的验证码！");
            }
        }

        private void NavigateToRegistrationPage()
        {
            string email = txtEmail.Text;
            RegisterPage register = new RegisterPage(email);
            register.Show();
            this.Close();
        }

        private void NavigateToMainPage()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void Btn_SendCode_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;

            if (LoginManager.ValidateEmail(email))
            {
                verificationCode = await Task.Run(() => EmailManager.SendVerificationCode(email));

                if (verificationCode != null)
                {
                    Growl.Success($"验证码已发送至 {email}。请查看您的邮箱。");

                    verificationCodeBorder.Visibility = Visibility.Visible;
                    SendCodeButton.Visibility = Visibility.Collapsed;
                    SignInByCodeButton.Visibility = Visibility.Visible;
                }
                else
                {
                    Growl.Error("发送验证码失败。请重试。");
                }
            }
            else
            {
                Growl.Error("请输入有效的邮箱地址！");
            }
        }

        private void Btn_Password_Click(object sender, RoutedEventArgs e)
        {
            passwordBorder.Visibility = Visibility.Visible;
            SignInByPasswordButton.Visibility = Visibility.Visible;
            CodeButton.Visibility = Visibility.Visible;

            PasswordButton.Visibility = Visibility.Collapsed;
            verificationCodeBorder.Visibility = Visibility.Collapsed;
            SendCodeButton.Visibility = Visibility.Collapsed;
            SignInByCodeButton.Visibility = Visibility.Collapsed;
        }

        private void Btn_Code_Click(object sender, RoutedEventArgs e)
        {
            passwordBorder.Visibility = Visibility.Collapsed;
            SignInByPasswordButton.Visibility = Visibility.Collapsed;
            CodeButton.Visibility = Visibility.Collapsed;

            PasswordButton.Visibility = Visibility.Visible;
            SendCodeButton.Visibility = Visibility.Visible;
            SignInByCodeButton.Visibility = Visibility.Visible;
        }

        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void TextBox_Email_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void PerformLogin()
        {
            bool success = LoginManager.PerformLogin(txtEmail.Text);
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
    }
}