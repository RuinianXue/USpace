using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using HandyControl.Controls;
using System.Windows.Media.Imaging;
using UIDisplay.Cards;
using UIDisplay.Pages;

namespace UIDisplay.Utils
{

    public class MenuBarMain : Menu
    {
        public Button button;
        public ContextMenu menu;
        public MenuBarMain()
        {

            button = new Button();
            button.Click += Button_Click;

            Image image = new Image();
            BitmapImage bitmap = new BitmapImage(new Uri("../Images/more.png", UriKind.RelativeOrAbsolute));
            image.Source = bitmap;
            image.Width = 20;
            image.Height = 20;

            button.Content = image;
            button.Click += Button_Click;
            menu = new ContextMenu();
            //public static string[] enumsofType = new string[6] { "arxiv", "battery", "todo", "weather", "tomato", "weathersmall" };
            MenuItem arxivItem = new MenuItem { Header = "Arxiv" };
            arxivItem.Click += ArxivItem_Click;
            menu.Items.Add(arxivItem);

            MenuItem batteryItem = new MenuItem { Header = "Battery" };
            batteryItem.Click += BatteryItem_Click;
            menu.Items.Add(batteryItem);

            MenuItem todoItem = new MenuItem { Header = "Todo" };
            todoItem.Click += TodoItem_Click;
            menu.Items.Add(todoItem);

            MenuItem weatherItem = new MenuItem { Header = "Weather" };
            weatherItem.Items.Add(CreateSubMenuItem("small", WeatherSmall_Click));
            weatherItem.Items.Add(CreateSubMenuItem("big", WeatherBig_Click));
            menu.Items.Add(weatherItem);

            MenuItem tomatoItem = new MenuItem { Header = "Tomato Clock" };
            tomatoItem.Items.Add(CreateSubMenuItem("medium", TomatoSmall_Click));
            tomatoItem.Items.Add(CreateSubMenuItem("big", TomatoBig_Click));
            menu.Items.Add(tomatoItem);
            /*
            MenuItem arxivItem = new MenuItem { Header = "Arxiv" };
            menu.Items.Add(arxivItem);

            MenuItem batteryItem = new MenuItem { Header = "Battery" };
            menu.Items.Add(batteryItem);

            MenuItem todoItem = new MenuItem { Header = "Todo" };
            menu.Items.Add(todoItem);

            MenuItem weatherItem = new MenuItem { Header = "Weather" };
            weatherItem.Items.Add(new MenuItem { Header = "small" });
            weatherItem.Items.Add(new MenuItem { Header = "big" });
            menu.Items.Add(weatherItem);

            MenuItem tomatoItem = new MenuItem { Header = "Tomato Clock" };
            tomatoItem.Items.Add(new MenuItem { Header = "medium" });
            tomatoItem.Items.Add(new MenuItem { Header = "big" });
            menu.Items.Add(tomatoItem);
            */
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            //button.Margin = new Thickness(10);
            button.Background = Brushes.Transparent;
            button.BorderThickness = new Thickness(0);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.menu.IsOpen = true;
            Dashboard.intoEditMode();
        }
        private void ArxivItem_Click(object sender, RoutedEventArgs e)
        {
            ArxivCard arxivCard = new ArxivCard();
            arxivCard.SetPosition(Dashboard.inGrid, 0, 0);
        }
        private void BatteryItem_Click(object sender, RoutedEventArgs e)
        {
            BatteryCard batteryCardSmall = new BatteryCard();
            batteryCardSmall.SetPosition(Dashboard.inGrid, 0, 0);

        }
        private void TodoItem_Click(object sender, RoutedEventArgs e)
        {
            TodoCard todoCard = new TodoCard();
            Dashboard.AddNewTodoCard(todoCard);
            todoCard.SetPosition(Dashboard.inGrid, 0, 0);

        }
        private void WeatherBig_Click(object sender, RoutedEventArgs e)
        {
            WeatherCardBig weatherCardBig = new WeatherCardBig();
            weatherCardBig.SetPosition(Dashboard.inGrid, 0, 0);

        }
        private void WeatherSmall_Click(object sender, RoutedEventArgs e)
        {
            WeatherCardSmall weatherCardSmall = new WeatherCardSmall();
            weatherCardSmall.SetPosition(Dashboard.inGrid, 0, 0);

        }
        private void TomatoBig_Click(object sender, RoutedEventArgs e)
        {
            TomatoCard tomatoCard = new TomatoCard();
            tomatoCard.SetPosition(Dashboard.inGrid, 0, 0);

        }
        private void TomatoSmall_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private MenuItem CreateSubMenuItem(string header, RoutedEventHandler clickHandler)
        {
            MenuItem subMenuItem = new MenuItem { Header = header };
            subMenuItem.Click += clickHandler;
            return subMenuItem;
        }
    }
}
