using UIDisplay.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIDisplay
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        TodolistPage todolistPage;
        Dashboard dashboardPage;
        public MainWindow()
        {
            InitializeComponent();

            WindowInitialize(); 
            //MenuInitialize();
        }

        private void MenuInitialize()
        {
            MenuButton btnMenu  = new MenuButton();
            Grid.SetRow(btnMenu, 0); // 将MenuButton添加到第二行
            gridMenu.Children.Add(btnMenu);
        }
        private void WindowInitialize()
        {
            this.Width = Constants.INSIDE_WIDTH + 80;
            this.Height = Constants.INSIDE_HEIGHT + 30;
            PagesNavigation.Height = Constants.INSIDE_HEIGHT;
            PagesNavigation.Width = Constants.INSIDE_WIDTH;
            todolistPage =new TodolistPage();
            dashboardPage = new Dashboard();
            PagesNavigation.Navigate(dashboardPage);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdTodolist_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(todolistPage);
        }
        private void dashboard_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(dashboardPage);

        }
    }
}
