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
    /// <summary>
    /// 登录页面，用于用户登录操作。
    /// </summary>
    public partial class LoginPage : System.Windows.Window
    {
        // 用于存储验证码的字符串
        string verificationCode = null;

        /// <summary>
        /// 初始化 <see cref="LoginPage"/> 类的新实例。
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 鼠标按下事件，用于实现窗口拖动。
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// 关闭按钮鼠标抬起事件，用于关闭应用程序。
        /// </summary>
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 密码框密码更改事件，用于控制显示或隐藏密码提示文本。
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 验证码框验证码更改事件，用于控制显示或隐藏验证码提示文本。
        /// </summary>
        private void VerificationCodeBox_CodeChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(verificationCodeBox.Password) && verificationCodeBox.Password.Length > 0)
                textVerificationCode.Visibility = Visibility.Collapsed;
            else
                textVerificationCode.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 密码提示文本鼠标点击事件，用于使密码框获得焦点。
        /// </summary>
        private void TextBlock_Password_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        /// <summary>
        /// 验证码提示文本鼠标点击事件，用于使验证码框获得焦点。
        /// </summary>
        private void TextBlock_VerificationCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            verificationCodeBox.Focus();
        }

        /// <summary>
        /// 通过密码登录按钮点击事件，用于验证密码并执行登录操作。
        /// </summary>
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

        /// <summary>
        /// 通过验证码登录按钮点击事件，用于验证验证码并执行登录操作。
        /// </summary>
        private void Btn_SignInByCode_Click(object sender, RoutedEventArgs e)
        {
            string input = verificationCodeBox.Password;
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(input))
            {
                Growl.Error("请输入验证码！");
                return;
            }

            // 检查邮箱是否已注册
            bool isEmailRegistered = LoginManager.CheckIfEmailIsRegistered(email);

            if (input == verificationCode)
            {
                if (!isEmailRegistered)
                {
                    // 如果邮箱未注册，则跳转至注册页面
                    NavigateToRegistrationPage();
                }
                else
                {
                    // 如果邮箱已注册，则执行登录操作
                    PerformLogin();
                }
            }
            else
            {
                Growl.Error("请输入有效的验证码！");
            }
        }

        /// <summary>
        /// 跳转至注册页面，并传递邮箱信息。
        /// </summary>
        private void NavigateToRegistrationPage()
        {
            string email = txtEmail.Text;
            RegisterPage register = new RegisterPage(email);
            register.Show();
            this.Close();
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

        /// <summary>
        /// 发送验证码按钮点击事件，用于发送验证码至用户邮箱。
        /// </summary>
        private async void Btn_SendCode_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;

            if (LoginManager.ValidateEmail(email))
            {
                // 使用异步任务发送验证码
                verificationCode = await Task.Run(() => EmailManager.SendVerificationCode(email));

                if (verificationCode != null)
                {
                    Growl.Success($"验证码已发送至 {email}。请查看您的邮箱。");

                    // 显示验证码输入相关的控件，隐藏发送验证码按钮
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

        /// <summary>
        /// 切换至密码登录按钮点击事件，用于显示密码输入相关的控件。
        /// </summary>
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

        /// <summary>
        /// 切换至验证码登录按钮点击事件，用于显示验证码输入相关的控件。
        /// </summary>
        private void Btn_Code_Click(object sender, RoutedEventArgs e)
        {
            passwordBorder.Visibility = Visibility.Collapsed;
            SignInByPasswordButton.Visibility = Visibility.Collapsed;
            CodeButton.Visibility = Visibility.Collapsed;
            SignInByCodeButton.Visibility = Visibility.Collapsed;

            PasswordButton.Visibility = Visibility.Visible;
            SendCodeButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 邮箱文本框文本更改事件，用于控制显示或隐藏邮箱提示文本。
        /// </summary>
        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 邮箱提示文本鼠标点击事件，用于使邮箱文本框获得焦点。
        /// </summary>
        private void TextBox_Email_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        /// <summary>
        /// 执行登录操作。
        /// </summary>
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
