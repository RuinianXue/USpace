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
using UIDisplay.Components;
using System.Configuration;
using HandyControl.Controls;
using HandyControl.Data;
using System.Windows.Input;

namespace UIDisplay.Cards
{
    //Type 3 & 5
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
    class InsideGrid : Grid
    {
        public InsideGrid()
        {

        }
        public InsideGrid(IconImage upimage, SmallInBigDisp downsmall)
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40, GridUnitType.Pixel) });
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30, GridUnitType.Pixel) });

            Grid.SetRow(upimage, 0);
            Grid.SetRow(downsmall, 1);
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
        ClickCardOfWeather clickCardOfWeather;
        protected string GetWeatherIcon()
        {
            return "";
        }
        private void refreshItem_Click(object sender, EventArgs e)
        {
            refreshWeather();
        }
        private void WuhanClick(object sender, EventArgs e)
        {
            this.placeChosen = "武汉";
            refreshWeather();
        }
        private void BeijingClick(object sender, EventArgs e)
        {
            this.placeChosen = "北京";
            refreshWeather();
        }
        private void ChongqingClick(object sender, EventArgs e)
        {
            this.placeChosen = "重庆";
            refreshWeather();
        }
        private void ShanghaiClick(object sender, EventArgs e)
        {
            this.placeChosen = "上海";
            refreshWeather();
        }
        protected override void MenuInitialize()
        {
            ContextMenu contextMenu = new ContextMenu();
            //MenuItem moveItem = new MenuItem();
            //moveItem.Header = "Move Card";
            //moveItem.Click += MoveItem_Click;
            //contextMenu.Items.Add(moveItem);
            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "Delete Card";
            deleteItem.Click += DeleteItem_Click;
            contextMenu.Items.Add(deleteItem);
            MenuItem refreshItem = new MenuItem();
            refreshItem.Header = "Refresh Weather";
            refreshItem.Click += refreshItem_Click;
            contextMenu.Items.Add(refreshItem);
            this.ContextMenu = contextMenu;
            MenuItem choosePlace = new MenuItem();
            choosePlace.Header = "Choose Place";

            choosePlace.Items.Add(CreateSubMenuItem("Wuhan", WuhanClick));
            choosePlace.Items.Add(CreateSubMenuItem("Chongqing", ChongqingClick));
            choosePlace.Items.Add(CreateSubMenuItem("Beijing", BeijingClick));
            choosePlace.Items.Add(CreateSubMenuItem("Shanghai", ShanghaiClick));

            //choosePlace.Click += choosePlaceClick;
            contextMenu.Items.Add(choosePlace);
            this.ContextMenu = contextMenu;
            this.MouseRightButtonDown += Card_MouseRightButtonDown;
        }
        private MenuItem CreateSubMenuItem(string header, RoutedEventHandler clickHandler)
        {
            MenuItem subMenuItem = new MenuItem { Header = header };
            subMenuItem.Click += clickHandler;
            return subMenuItem;
        }
        private static BlurMask blurmask = new BlurMask(Dashboard.mainGrid, Dashboard.outGrid);
        protected override void ClickCardInitialize()
        {
            MouseDoubleClick += Card_DoubleClick;
            clickCardOfWeather = new ClickCardOfWeather(this.placeChosen) ;
            blurmask.MaskClicked += Mask_ClickClose;
        }
        protected override void Mask_ClickClose(object sender, EventArgs e)
        {
            blurmask.Disappear(Dashboard.outGrid);
            clickCardOfWeather.Disappear(Dashboard.overallGrid);
        }
        protected override void Card_DoubleClick(object sender, EventArgs e)
        {
            blurmask.Appear(Dashboard.outGrid);
            clickCardOfWeather.Appear(Dashboard.overallGrid);
        }
        public override void SetPosition(Grid grid, int row, int colomn)
        {
            base.SetPosition(grid, row, colomn);
            IgnoredCard tmp = new IgnoredCard(this, 5,this.placeChosen);
            Dashboard.loadDashJson.AddCard(tmp);
        }
        private void Initialize()
        {
            MenuInitialize();
            ClickCardInitialize();
            stackPanel = new StackPanel();
            stackPanel.Width = this.Width - 25;
            stackPanel.Height = this.Height - 25;
            stackPanel.Children.Clear();
            stackPanel.Orientation = Orientation.Vertical;
            typeOfCard = 5;
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
        public void refreshWeather()
        {
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
    public class WeatherCardBig : BigSquareCard
    {
        protected string placeChosen;
        protected WeatherAnalysis weatherAnalysis;
        protected string weatherdisp;
        private Grid dispTopWeatherGrid;
        private Grid dispSecondWeatherGrid;
        private Grid dispThirdWeatherGrid;
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
            SmallDisp icon = new SmallDisp("  ");
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

            IconImage humidity = new IconImage("humidity");
            SmallInBigDisp hum = new SmallInBigDisp(weatherAnalysis.Humidity);
            InsideGrid s1 = new InsideGrid(humidity, hum);

            IconImage uvindex = new IconImage("uvindex");
            SmallInBigDisp uv = new SmallInBigDisp(weatherAnalysis.UVIndex);
            InsideGrid s2 = new InsideGrid(uvindex, uv);

            IconImage windpower = new IconImage("windpower");
            SmallInBigDisp wp = new SmallInBigDisp(weatherAnalysis.WindPower);
            InsideGrid s3 = new InsideGrid(windpower, wp);


            IconImage winddescription = new IconImage("winddescription");
            SmallInBigDisp wd = new SmallInBigDisp(weatherAnalysis.WindDescription);
            InsideGrid s4 = new InsideGrid(winddescription, wd);

            IconImage precipitation = new IconImage("precipitation");
            SmallInBigDisp perc = new SmallInBigDisp(weatherAnalysis.Precipitation);
            InsideGrid s5 = new InsideGrid(precipitation, perc);

            IconImage visibility = new IconImage("visibility");
            SmallInBigDisp vis = new SmallInBigDisp(weatherAnalysis.Visibility);
            InsideGrid s6 = new InsideGrid(visibility, vis);

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
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            // 创建 Poptip 控件
            Poptip poptip = new Poptip();
            poptip.Content = "Your Tooltip Content";
            poptip.PlacementType = PlacementType.Bottom;


            // 将 Poptip 添加到 mainGrid 上
            //dispThirdWeatherGrid.Children.Add(poptip);


            // 设置 Poptip 的位置
            Point position = e.GetPosition(dispThirdWeatherGrid);
            Canvas.SetLeft(poptip, position.X);
            Canvas.SetTop(poptip, position.Y);
            //poptip.IsOpen = true;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            // 移除 Poptip 控件
            Poptip poptip = dispThirdWeatherGrid.Children.OfType<Poptip>().FirstOrDefault();
            if (poptip != null)
            {
                poptip.IsOpen = false;
                dispThirdWeatherGrid.Children.Remove(poptip);
            }
        }
        private void ThirdPart()
        {
            dispThirdWeatherGrid = new Grid();
            dispThirdWeatherGrid.Margin = new Thickness(-5, 15, 0, 0);
            ColumnDefinition tmpGridColumnDef1 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef2 = new ColumnDefinition();
            ColumnDefinition tmpGridColumnDef3 = new ColumnDefinition();
            tmpGridColumnDef1.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef2.Width = new GridLength(1, GridUnitType.Star);
            tmpGridColumnDef3.Width = new GridLength(1, GridUnitType.Star);
            dispThirdWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef1);
            dispThirdWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef2);
            dispThirdWeatherGrid.ColumnDefinitions.Add(tmpGridColumnDef3);

            IconImage clothes = new IconImage("clothes");
            SmallInBigDisp clo = new SmallInBigDisp("");
            InsideGrid s1 = new InsideGrid(clothes, clo);
            //clothes.MouseEnter += Image_MouseEnter;
            //clothes.MouseLeave += Image_MouseLeave;
            IconImage life = new IconImage("life");
            SmallInBigDisp lf = new SmallInBigDisp("");
            InsideGrid s2 = new InsideGrid(life, lf);

            IconImage travel = new IconImage("travel");
            SmallInBigDisp tv = new SmallInBigDisp("");
            InsideGrid s3 = new InsideGrid(travel, tv);

            Grid.SetColumn(s1, 0);
            Grid.SetColumn(s2, 1);
            Grid.SetColumn(s3, 2);

            dispThirdWeatherGrid.Children.Add(s1);
            dispThirdWeatherGrid.Children.Add(s2);
            dispThirdWeatherGrid.Children.Add(s3);

            stackPanel.Children.Add(dispThirdWeatherGrid);

        }
        private void refreshItem_Click(object sender, EventArgs e)
        {
            refreshWeather();
        }
        private void WuhanClick(object sender, EventArgs e)
        {
            this.placeChosen = "武汉";
            refreshWeather();
        }
        private void BeijingClick(object sender, EventArgs e)
        {
            this.placeChosen = "北京";
            refreshWeather();
        }
        private void ChongqingClick(object sender, EventArgs e)
        {
            this.placeChosen = "重庆";
            refreshWeather();
        }
        private void ShanghaiClick(object sender, EventArgs e)
        {
            this.placeChosen = "上海";
            refreshWeather();
        }
        protected override void MenuInitialize()
        {
            ContextMenu contextMenu = new ContextMenu();
            //MenuItem moveItem = new MenuItem();
            //moveItem.Header = "Move Card";
            //moveItem.Click += MoveItem_Click;
            //contextMenu.Items.Add(moveItem);
            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "Delete Card";
            deleteItem.Click += DeleteItem_Click;
            contextMenu.Items.Add(deleteItem);
            MenuItem refreshItem = new MenuItem();
            refreshItem.Header = "Refresh Weather";
            refreshItem.Click += refreshItem_Click;
            contextMenu.Items.Add(refreshItem);
            this.ContextMenu = contextMenu;
            MenuItem choosePlace = new MenuItem();
            choosePlace.Header = "Choose Place";

            choosePlace.Items.Add(CreateSubMenuItem("Wuhan", WuhanClick));
            choosePlace.Items.Add(CreateSubMenuItem("Chongqing", ChongqingClick));
            choosePlace.Items.Add(CreateSubMenuItem("Beijing", BeijingClick));
            choosePlace.Items.Add(CreateSubMenuItem("Shanghai", ShanghaiClick));

            //choosePlace.Click += choosePlaceClick;
            contextMenu.Items.Add(choosePlace);
            this.ContextMenu = contextMenu;
            this.MouseRightButtonDown += Card_MouseRightButtonDown;
        }
        public void refreshWeather()
        {
            GetWeatherData();
        }
        private MenuItem CreateSubMenuItem(string header, RoutedEventHandler clickHandler)
        {
            MenuItem subMenuItem = new MenuItem { Header = header };
            subMenuItem.Click += clickHandler;
            return subMenuItem;
        }
        private void Initialize()
        {
            typeOfCard = 3;
            MenuInitialize();
            //ClickCardInitialize();
            stackPanel = new StackPanel();
            stackPanel.Width = this.Width - 40;
            stackPanel.Height = this.Height - 40;
            stackPanel.Children.Clear();
            stackPanel.Orientation = Orientation.Vertical;

            FirstPart();
            SecondPart();
            ThirdPart();
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
            this.placeChosen = "湖北武汉";
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
            IgnoredCard tmp = new IgnoredCard(this, 3,this.placeChosen);
            Dashboard.loadDashJson.AddCard(tmp);
        }
    }
    public class ClickCardOfWeather : WeatherCardBig
    {
        private Grid gridOfClickCard;
        public void Appear(Grid overallGrid)
        {
            if (overallGrid.Children.Contains(gridOfClickCard))
            {
                overallGrid.Children.Remove(gridOfClickCard);
            }
            overallGrid.Children.Add(gridOfClickCard);
        }
        public void Disappear(Grid overallGrid)
        {
            if (overallGrid.Children.Contains(gridOfClickCard))
            {
                overallGrid.Children.Remove(gridOfClickCard);
            }
        }
        public ClickCardOfWeather()
        {
            gridOfClickCard = new Grid();
            Panel.SetZIndex(gridOfClickCard, 1);
            BorderThickness = new Thickness(5);
            gridOfClickCard.Children.Add(this);
        }
        public ClickCardOfWeather(string place)
        {
            this.placeChosen = place;
            gridOfClickCard = new Grid();
            Panel.SetZIndex(gridOfClickCard, 1);
            BorderThickness = new Thickness(5);
            gridOfClickCard.Children.Add(this);
        }
        public override void SetPosition(Grid grid, int row, int colomn)
        {
            //base.SetPosition(grid, row, colomn);
        }
    }
}
