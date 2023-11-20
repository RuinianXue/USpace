using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace LoginTest
{
    public partial class Login : Window
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

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string enteredCode = passwordBox.Password;

            if (string.IsNullOrEmpty(enteredCode))
            {
                MessageBox.Show("Please enter the verification code.");
                return;
            }

            MessageBox.Show($"Entered Code: {enteredCode}\nVerification Code: {verificationCode}");

            // Check if the entered code matches the one sent by EmailService
            if (enteredCode == verificationCode)
            {
                // Login successful
                MessageBox.Show("Successfully logged in!");
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.");
            }
        }

        private void ButtonSendCode_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            if (IsValidEmail(email))
            {
                verificationCode = EmailService.SendVerificationCode(email);

                if (verificationCode != null)
                {
                    // Verification code sent successfully, you can use the verificationCode as needed
                    MessageBox.Show($"Verification code sent to {email}. Check your email.");
                }
                else
                {
                    // Handle error sending verification code
                    MessageBox.Show("Failed to send verification code. Please try again.");
                }
            }
            else
            {
                // Handle invalid email format
                MessageBox.Show("Invalid email format. Please enter a valid email address.");
            }
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
    }
}