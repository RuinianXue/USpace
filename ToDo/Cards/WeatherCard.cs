using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIDisplay.WeatherCrawl;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using UIDisplay.Pages;
using UIDisplay.Utils;

namespace UIDisplay.Cards
{
    //Type 4
    class SmallDisp : TextBlock
    {
        public SmallDisp()
        {
            this.FontSize = 18;
            this.FontFamily = (FontFamily)Application.Current.Resources["PingFang"];
            this.Text = "";
        }
        public SmallDisp(string str)
        {
            this.FontSize = 18;
            this.FontFamily = (FontFamily)Application.Current.Resources["PingFang"];
            this.Text = str;
        }
    }
    class InsideStackPanel : StackPanel
    {
        public InsideStackPanel()
        {

        }
        public InsideStackPanel(IconImage upimage, SmallInBigDisp downsmall)
        {
            this.Orientation = Orientation.Vertical;
            this.Children.Add(upimage);
            this.Children.Add(downsmall);
        }
    }
    class SmallInBigDisp : SmallDisp
    {
        public SmallInBigDisp()
        {
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.Margin = new Thickness(0, 10, 0, 0);
            this.FontSize = 13;
        }
        public SmallInBigDisp(string str)
        {
            this.Text = str;
            this.FontSize = 13;
            this.Margin = new Thickness(0, 10, 0, 0);
            this.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }
    class MediumDisp : SmallDisp
    {
        public MediumDisp()
        {
            this.FontSize = 35;
        }
        public MediumDisp(string str) : base(str)
        {
            this.FontSize = 35;
        }
    }
    internal class WeatherCardSmall : SmallSquareCard
    {
        protected string placeChosen;
        protected WeatherAnalysis weatherAnalysis;
        protected string weatherdisp;
        protected string GetWeatherIcon()
        {
            return "";
        }
        private void Initialize()
        {
            ClickCardInitialize();
            stackPanel = new StackPanel();
            stackPanel.Width = this.Width - 25;
            stackPanel.Height = this.Height - 25;
            stackPanel.Children.Clear();
            stackPanel.Orientation = Orientation.Vertical;

            weatherdisp = weatherAnalysis.Weather;

            SmallDisp placedisp = new SmallDisp(this.placeChosen);
            MediumDisp tempdisp = new MediumDisp(weatherAnalysis.Temperature + "°");
            SmallDisp wth = new SmallDisp(this.weatherdisp);
            SmallDisp temprangedisp = new SmallDisp("L:" + weatherAnalysis.TempRange_low + "  " + "H:" + weatherAnalysis.TempRange_high);

            stackPanel.Children.Add(placedisp);
            stackPanel.Children.Add(tempdisp);
            stackPanel.Children.Add(wth);
            stackPanel.Children.Add(temprangedisp);
            Content = stackPanel;
        }
        private async void GetWeatherData()
        {
            weatherAnalysis = new WeatherAnalysis(this.placeChosen);
            await weatherAnalysis.getAnalysis();
            Initialize();
        }
        public WeatherCardSmall()
        {
            this.placeChosen = "武汉";
            GetWeatherData();
        }
        public WeatherCardSmall(string Place)
        {
            this.placeChosen = Place;
            GetWeatherData();
        }
    }
    class IconImage : Image
    {
        const string pathOfIcon = "../Images/WeatherCard/";
        public IconImage()
        {
            BitmapImage bitmapArxiv = new BitmapImage(new Uri(pathOfIcon + "humidity.png", UriKind.RelativeOrAbsolute));
            this.Source = bitmapArxiv;
            this.Width = 30;//46
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        public IconImage(string title)
        {
            BitmapImage bitmapArxiv = new BitmapImage(new Uri(pathOfIcon + title + ".png", UriKind.RelativeOrAbsolute));
            this.Source = bitmapArxiv;
            this.Width = 30;//46
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
    }
    internal class WeatherCardBig : BigSquareCard
    {
        protected string placeChosen;
        protected WeatherAnalysis weatherAnalysis;
        protected string weatherdisp;
        private Grid dispTopWeatherGrid;
        private Grid dispSecondWeatherGrid;
        private void FirstPart()
        {
            dispTopWeatherGrid = new Grid();
            ColumnDefinition tmpGridColumnDef1 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef2 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef3 = new ColumnDefinition();
            tmpGridColumnDef1.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef2.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef3.Width = new GridLength(1, GridUnitType.Star);
            dispTopWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef1);
            dispTopWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef2);
            dispTopWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef3);

            //dispTopWeatherGrid.ColumnDefinitions(dispTopWeatherGrid.Width, dispTopWeatherGrid.Height,)
            weatherdisp = weatherAnalysis.Weather;

            SmallDisp placedisp = new SmallDisp(this.placeChosen);
            MediumDisp tempdisp = new MediumDisp(weatherAnalysis.Temperature + "°");
            SmallDisp wth = new SmallDisp(this.weatherdisp);
            SmallDisp temprangedisp = new SmallDisp("L:" + weatherAnalysis.TempRange_low + "  " + "H:" + weatherAnalysis.TempRange_high);
            SmallDisp icon = new SmallDisp("☀️");
            StackPanel smallnew1 = new StackPanel();
            smallnew1.Orientation = Orientation.Vertical;
            smallnew1.Children.Clear();
            smallnew1.Children.Add(placedisp);
            smallnew1.Children.Add(tempdisp);
            Grid.SetColumn(smallnew1, 0);
            dispTopWeatherGrid.Children.Add(smallnew1);

            StackPanel smallnew2 = new StackPanel();
            smallnew2.Orientation = Orientation.Vertical;
            smallnew2.Children.Clear();
            icon.HorizontalAlignment = HorizontalAlignment.Right;
            wth.HorizontalAlignment = HorizontalAlignment.Right;
            temprangedisp.HorizontalAlignment = HorizontalAlignment.Right;

            smallnew2.Children.Add(icon);
            smallnew2.Children.Add(wth);
            smallnew2.Children.Add(temprangedisp);
            Grid.SetColumn(smallnew2, 2);
            dispTopWeatherGrid.Children.Add(smallnew2);

            Line horizontalLine = new Line();

            horizontalLine.X1 = 0;
            horizontalLine.Y1 = 20;
            horizontalLine.X2 = 280;
            horizontalLine.Y2 = 20;
            horizontalLine.Stroke = Brushes.LightGray;
            horizontalLine.StrokeThickness = 2;


            stackPanel.Children.Add(dispTopWeatherGrid);
            stackPanel.Children.Add(horizontalLine);
        }
        private void SecondPart()
        {
            dispSecondWeatherGrid = new Grid();
            dispSecondWeatherGrid.Margin = new Thickness(-5, 15, 0, 0);
            ColumnDefinition tmpGridColumnDef1 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef2 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef3 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef4 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef5 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef6 = new ColumnDefinition();
            tmpGridColumnDef1.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef2.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef3.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef4.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef5.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef6.Width = new GridLength(1, GridUnitType.Star);
            dispSecondWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef1);
            dispSecondWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef2);
            dispSecondWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef3);
            dispSecondWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef4);
            dispSecondWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef5);
            dispSecondWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef6);
            /*
            RowDefinition tmpRowDef1 = new RowDefinition();
            RowDefinition tmpRowDef2 = new RowDefinition();
            tmpRowDef1.Height = new GridLength(3, GridUnitType.Star);
            tmpRowDef2.Height = new GridLength(1, GridUnitType.Star);
            */

            IconImage humidity = new IconImage("humidity");
            SmallInBigDisp hum = new SmallInBigDisp(weatherAnalysis.Humidity);
            InsideStackPanel s1 = new InsideStackPanel(humidity,hum);

            IconImage uvindex = new IconImage("uvindex");
            SmallInBigDisp uv = new SmallInBigDisp(weatherAnalysis.UVIndex);
            InsideStackPanel s2 = new InsideStackPanel(uvindex, uv);

            IconImage windpower = new IconImage("windpower");
            SmallInBigDisp wp = new SmallInBigDisp(weatherAnalysis.WindPower);
            InsideStackPanel s3 = new InsideStackPanel(windpower, wp);


            IconImage winddescription = new IconImage("winddescription");
            SmallInBigDisp wd = new SmallInBigDisp(weatherAnalysis.WindDescription);
            InsideStackPanel s4 = new InsideStackPanel(winddescription, wd);

            IconImage precipitation = new IconImage("precipitation");
            SmallInBigDisp perc = new SmallInBigDisp(weatherAnalysis.Precipitation);
            InsideStackPanel s5 = new InsideStackPanel(precipitation, perc);

            IconImage visibility = new IconImage("visibility");
            SmallInBigDisp vis = new SmallInBigDisp(weatherAnalysis.Visibility);
            InsideStackPanel s6 = new InsideStackPanel(visibility, vis);

            /*
            UVIndex
            WindPower
            WindDescription
            Precipitation
            Humidity
            Pressure
            Visibility
            ../Images/WeatherCard/
            */
            Grid.SetColumn(s1, 0);
            Grid.SetColumn(s2, 1);
            Grid.SetColumn(s3, 2);
            Grid.SetColumn(s4, 3);
            Grid.SetColumn(s5, 4);
            Grid.SetColumn(s6, 5);

            dispSecondWeatherGrid.Children.Add(s1);
            dispSecondWeatherGrid.Children.Add(s2);
            dispSecondWeatherGrid.Children.Add(s3);
            dispSecondWeatherGrid.Children.Add(s4);
            dispSecondWeatherGrid.Children.Add(s5);
            dispSecondWeatherGrid.Children.Add(s6);
            /*
            dispSecondWeatherGrid.Children.Add(hum);
            dispSecondWeatherGrid.Children.Add(uv);
            dispSecondWeatherGrid.Children.Add(wp);
            dispSecondWeatherGrid.Children.Add(wd);
            dispSecondWeatherGrid.Children.Add(perc);
            dispSecondWeatherGrid.Children.Add(vis);*/
            stackPanel.Children.Add(dispSecondWeatherGrid);

            Line horizontalLine = new Line();

            horizontalLine.X1 = 0;
            horizontalLine.Y1 = 20;
            horizontalLine.X2 = 280;
            horizontalLine.Y2 = 20;
            horizontalLine.Stroke = Brushes.LightGray;
            horizontalLine.StrokeThickness = 2;

            stackPanel.Children.Add(horizontalLine);

        }
        private void Initialize()
        {
            ClickCardInitialize();
            stackPanel = new StackPanel();
            stackPanel.Width = this.Width - 40;
            stackPanel.Height = this.Height - 40;
            stackPanel.Children.Clear();
            stackPanel.Orientation = Orientation.Vertical;

            FirstPart();
            SecondPart();
            Content = stackPanel;
        }
        private async void GetWeatherData()
        {
            weatherAnalysis = new WeatherAnalysis(this.placeChosen);
            await weatherAnalysis.getAnalysis();
            Initialize();
        }
        public WeatherCardBig()
        {
            this.placeChosen = "武汉";
            GetWeatherData();
        }
        public WeatherCardBig(string place)
        {
            this.placeChosen = place;
            GetWeatherData();
        }
        public override void SetPosition(Grid grid, int row, int colomn)
        {
            base.SetPosition(grid, row, colomn);
            IgnoredCard tmp = new IgnoredCard(this, 4);
            Dashboard.loadDashJson.AddCard(tmp);
        }
    }
}
