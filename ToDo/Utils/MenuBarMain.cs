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
            //arxivItem.Items.Add(new MenuItem { Header = "Big" });
            //arxivItem.Items.Add(new MenuItem { Header = "" });
            //arxivItem.Items.Add(new MenuItem { Header = "Exit" });
            menu.Items.Add(arxivItem);

            MenuItem batteryItem = new MenuItem { Header = "Battery" };
            //fileMenuItem.Items.Add(new MenuItem { Header = "Exit" });
            menu.Items.Add(batteryItem);

            MenuItem todoItem = new MenuItem { Header = "Todo" };
            //fileMenuItem.Items.Add(new MenuItem { Header = "Exit" });
            menu.Items.Add(todoItem);

            MenuItem weatherItem = new MenuItem { Header = "Weather" };
            weatherItem.Items.Add(new MenuItem { Header = "small" });
            weatherItem.Items.Add(new MenuItem { Header = "big" });
            menu.Items.Add(weatherItem);

            MenuItem tomatoItem = new MenuItem { Header = "Tomato Clock" };
            tomatoItem.Items.Add(new MenuItem { Header = "medium" });
            tomatoItem.Items.Add(new MenuItem { Header = "big" });
            menu.Items.Add(tomatoItem);

            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Margin = new Thickness(10);
            button.Background = Brushes.Transparent;
            button.BorderThickness = new Thickness(0);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.menu.IsOpen = true;
        }
    }
}
