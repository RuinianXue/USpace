using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System;
using UIDisplay.UserControls;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using UIDisplay.BLL;
using HandyControl.Controls;
using System.Data;

namespace UIDisplay.Pages
{
    public partial class Login : System.Windows.Window
    {
        string verificationCode = null;

        public Login()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void textVerificationCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            verificationCodeBox.Focus();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            // 创建注册窗口实例
            Register registerWindow = new Register();

            // 显示注册窗口
            registerWindow.Show();

            // Start the fade out animation
            //fadeOutAnimation.Begin();

            //// Get the storyboard
            //Storyboard fadeOutAnimation = this.FindResource("FadeOutAnimation") as Storyboard;

            //// Set the target of the storyboard
            //Storyboard.SetTarget(fadeOutAnimation, signUpBorder);

            //// Start the storyboard
            //fadeOutAnimation.Begin();

            //// Create a new instance of MyRegisterForm
            //MyRegisterForm registerControl = new MyRegisterForm();

            //// Add the new MyRegisterForm to column 1 after the animation completes
            //fadeOutAnimation.Completed += (s, eventArgs) =>
            //{
            //    // Remove the original content in column 1
            //    loginGrid.Children.Remove(signUpBorder);

            //    // Add the new MyRegisterForm to column 1
            //    Grid.SetColumn(registerControl, 1);
            //    loginGrid.Children.Add(registerControl);
            //};

            //// Create a new instance of RegisterUserControl
            //MyRegisterForm registerControl = new MyRegisterForm();

            //// Move the original content from column 0 to column 1
            //Grid.SetColumn(signUpBorder, 1);

            //// Add the new RegisterUserControl to column 0
            //Grid.SetColumn(registerControl, 0);
            //loginGrid.Children.Add(registerControl);

            //// Perform the slide animation as before
            //DoubleAnimation slideAnimation = new DoubleAnimation
            //{
            //    To = +this.ActualWidth, // Move the grid to the left by its own width
            //    Duration = TimeSpan.FromSeconds(0.5) // Duration of the animation
            //};
            ////SlideTransform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);
            //signUpBorder.RenderTransform = new TranslateTransform();
            //signUpBorder.RenderTransform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);
        }

        private void ButtonSignInByPassword_Click(object sender, RoutedEventArgs e)
        {
            string input = passwordBox.Password;

            if (string.IsNullOrEmpty(input))
            {
                Growl.Error("Please enter the password.");
                return;
            }

            string email = txtEmail.Text;
            string password = passwordBox.Password;

            // 验证用户输入的密码是否正确
            if (UserManager.VerifyUserPassword(email, password))
            {
                // 密码正确，执行登录操作
                // Your login logic here
                Growl.Success("Successfully logged in!");
                NavigateToMainPage(); // Assuming you have a function to navigate to the main page
            }
            else
            {
                // 密码错误
                Growl.Error("Invalid email or password. Please try again.");
            }
        }

        private void ButtonSignInByCode_Click(object sender, RoutedEventArgs e)
        {
            string input = verificationCodeBox.Password;
            string email = txtEmail.Text; // Assuming txtEmail is the TextBox for entering email

            if (string.IsNullOrEmpty(input))
            {
                Growl.Error("Please enter the verification code.");
                return;
            }

            // Check if the email is registered
            bool isEmailRegistered = CheckIfEmailIsRegistered(email);

            if (!isEmailRegistered)
            {
                NavigateToRegistrationPage();
            }
            else
            {
                // Email is registered, proceed with login
                if (input == verificationCode)
                {
                    // Login successful
                    Growl.Success("Successfully logged in!");
                    // Navigate to main page
                    NavigateToMainPage();
                }
                else
                {
                    Growl.Error("Invalid verification code. Please try again.");
                }
            }
        }

        private bool CheckIfEmailIsRegistered(string email)
        {
            DataTable result;
            return UserManager.SearchUser(email, out result);
        }

        private void NavigateToRegistrationPage()
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void NavigateToMainPage()
        {
            // 创建主页面实例
            MainWindow mainWindow = new MainWindow();

            // 导航到主页面
            mainWindow.Show();

            this.Close();
        }

        private void ButtonSendCode_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;

            if (IsValidEmail(email))
            {
                verificationCode = EmailManager.SendVerificationCode(email);

                if (verificationCode != null)
                {
                    // Verification code sent successfully, you can use the verificationCode as needed
                    Growl.Success($"Verification code sent to {email}. Check your email.");

                    //passwordBorder.Visibility = Visibility.Collapsed;
                    //PasswordButton.Visibility = Visibility.Collapsed;
                    verificationCodeBorder.Visibility = Visibility.Visible;
                    SendCodeButton.Visibility = Visibility.Collapsed;
                    SignInByCodeButton.Visibility = Visibility.Visible;

                    // Start the countdown
                    StartCountdownTimer();
                }
                else
                {
                    // Handle error sending verification code
                    Growl.Error("Failed to send verification code. Please try again.");
                }
            }
            else
            {
                // Handle invalid email format
                Growl.Error("Invalid email format. Please enter a valid email address.");
            }
        }

        private void ButtonPassword_Click(object sender, RoutedEventArgs e)
        {
            passwordBorder.Visibility = Visibility.Visible;
            SignInByPasswordButton.Visibility = Visibility.Visible;

            PasswordButton.Visibility = Visibility.Collapsed;
            verificationCodeBorder.Visibility = Visibility.Collapsed;
            SendCodeButton.Visibility = Visibility.Collapsed;
            SignInByCodeButton.Visibility = Visibility.Collapsed;
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private bool IsValidEmail(string email)
        {
            // Use a simple regex pattern for email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private void StartCountdownTimer()
        {
            int countdownSeconds = 60;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                countdownSeconds--;

                if (countdownSeconds > 0)
                {
                    // Update the CountdownLabel
                    CountdownLabel.Text = $"{countdownSeconds} seconds left to resend";

                    // Disable the label during countdown
                    CountdownLabel.IsEnabled = false;
                }
                else
                {
                    // Countdown is complete
                    timer.Stop();

                    // Enable the label and reset the CountdownLabel
                    CountdownLabel.IsEnabled = true;
                    CountdownLabel.Text = "Send Code";
                }
            };

            // Start the timer
            timer.Start();
        }
    }
}