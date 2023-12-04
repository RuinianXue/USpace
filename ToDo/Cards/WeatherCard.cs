using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIDisplay.WeatherCrawl;

namespace UIDisplay.Cards
{
    internal class WeatherCardSmall : SmallSquareCard
    {
        private string placeChosen;
        private WeatherAnalysis weatherAnalysis;
        private string weatherdisp;
        private string GetWeatherIcon()
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
            TextBlock placedisp = new TextBlock();
            placedisp.Text = this.placeChosen;
            placedisp.FontSize = 18;
            placedisp.FontFamily = (FontFamily)Application.Current.Resources["PingFang"]; ;
            TextBlock tempdisp = new TextBlock();
            tempdisp.Text = weatherAnalysis.Temperature + "°";
            tempdisp.FontSize = 35;
            //tempdisp.FontWeight = FontWeights.Bold;
            tempdisp.FontFamily = (FontFamily)Application.Current.Resources["PingFang"];
            Console.WriteLine(tempdisp.Text);
            weatherdisp = weatherAnalysis.Weather;
            TextBlock wth = new TextBlock();
            wth.Text = this.weatherdisp;
            wth.FontSize = 18;
            wth.FontFamily = (FontFamily)Application.Current.Resources["PingFang"];
            TextBlock temprangedisp = new TextBlock();
            temprangedisp.Text = "L:" + weatherAnalysis.TempRange_low + "  "+ "H:" + weatherAnalysis.TempRange_high;
            temprangedisp.FontFamily = (FontFamily)Application.Current.Resources["PingFang"];
            temprangedisp.FontSize = 18;


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
            //Console.WriteLine("!!!!!!!!!!!!!!!!!!");
            Initialize();
            //Console.WriteLine(weatherAnalysis.Temperature);
            /*
             * Weather
             * Temperature
             * Description
             * TempRange_low
             * TempRange_high
             */

        }
        public WeatherCardSmall()
        {
            this.placeChosen = "武汉";
            GetWeatherData();
            //Initialize();
        }
        public WeatherCardSmall(string Place)
        {
            this.placeChosen = Place;
            GetWeatherData();
            //Initialize();
        }
    }
    internal class WeatherCardBig : BigSquareCard
    {
        private string placeChosen;
        private WeatherAnalysis weatherAnalysis;
        private string weatherdisp;
        private string GetWeatherIcon()
        {
            return "";
        }
        private void Initialize()
        {
            ClickCardInitialize();
            stackPanel = new StackPanel();
            stackPanel.Width = this.Width - 20;
            stackPanel.Height = this.Height - 20;
            stackPanel.Children.Clear();
            stackPanel.Orientation = Orientation.Vertical;
            TextBlock placedisp = new TextBlock();
            placedisp.Text = this.placeChosen; ;
            TextBlock tempdisp = new TextBlock();
            tempdisp.Text = weatherAnalysis.Temperature;
            Console.WriteLine(tempdisp.Text);
            weatherdisp = weatherAnalysis.Weather;
            TextBlock wth = new TextBlock();
            wth.Text = this.weatherdisp;
            TextBlock temprangedisp = new TextBlock();
            temprangedisp.Text = "L:" + weatherAnalysis.TempRange_low + "  " + "H:" + weatherAnalysis.TempRange_high;


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
            //Console.WriteLine("!!!!!!!!!!!!!!!!!!");
            Initialize();
            //Console.WriteLine(weatherAnalysis.Temperature);
            /*
             * Weather
             * Temperature
             * Description
             * TempRange_low
             * TempRange_high
             */

        }
        public WeatherCardBig()
        {
            this.placeChosen = "武汉";
            GetWeatherData();
            //Initialize();
        }
        public WeatherCardBig(string Place)
        {
            this.placeChosen = Place;
            GetWeatherData();
            //Initialize();
        }
    }
}
