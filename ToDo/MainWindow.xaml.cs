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
using UIDisplay.Utils;

namespace UIDisplay
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        AddressbookPage addressbookPage;
        TodoListPage todolistPage;
        Dashboard dashboardPage;
        MarkdownEditorPage markdownEditor;
        public void todoRedirect(object sender, EventArgs e)
        {
            PagesNavigation.Navigate(todolistPage);
            Dashboard.IsChecked = false;
            rdTodolist.IsChecked = true;
        }
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
            addressbookPage = new AddressbookPage();
            todolistPage =new TodoListPage();
            dashboardPage = new Dashboard();
            dashboardPage.TodoCardDoubleClicked_Dash += todoRedirect;
            markdownEditor = new MarkdownEditorPage();
            /*
            #region Dashboard Menu
            ContextMenu contextMenu = new ContextMenu();

            MenuItem editModeMenuItem = new MenuItem();
            editModeMenuItem.Header = "Edit Mode";
            editModeMenuItem.Click += EditModeMenuItem_Click;
            contextMenu.Items.Add(editModeMenuItem);

            MenuItem viewDeletedMenuItem = new MenuItem();
            viewDeletedMenuItem.Header = "Recently Deleted";
            viewDeletedMenuItem.Click += ViewDeletedMenuItem_Click;
            contextMenu.Items.Add(viewDeletedMenuItem);

            this.ContextMenu = contextMenu;
            this.MouseRightButtonDown += MainPage_MouseRightButtonDown;
            #endregion
            */
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

        private void rdAddressbook_Click(object sender, RoutedEventArgs e)
        {
            now_dashboard = false;
            PagesNavigation.Navigate(addressbookPage);
        }

        private void rdTodolist_Click(object sender, RoutedEventArgs e)
        {
            now_dashboard = false;
            PagesNavigation.Navigate(todolistPage);
        }
        private void markdownEditor_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(markdownEditor);
        }
        bool now_dashboard = false;
        private void dashboard_Click(object sender, RoutedEventArgs e)
        {
            now_dashboard = true;
            PagesNavigation.Navigate(dashboardPage);
        }
        private void MainPage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(now_dashboard)
            {
                this.ContextMenu.IsOpen = true;
            }
        }

        private void EditModeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 在这里处理进入编辑模式的逻辑
        }

        private void ViewDeletedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // 在这里处理查看最近删除的逻辑
        }

    }
}
