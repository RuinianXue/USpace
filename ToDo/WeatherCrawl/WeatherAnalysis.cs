using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UIDisplay.WeatherCrawl
{
    public class WeatherAnalysis
    {
        private string placeOfChoose;
        private string filePath;
        private string inputFileString;

        private string weatherLine;
        private string weatherPM25Line;
        private string weatherFeatureLine;
        private const string pattern_dataline = @"data\[""weather""\]=(.*?);";
        //private const string pattern_data = @"\""bodytemp_info\"":\""(?<bodytemp_info>[^\""]+)\"".*\""wind_direction\"":\""(?<wind_direction>[^\""]+)\"".*\""weather\"":\""(?<weather>[^\""]+)\"".*\""dew_temperature\"":\""(?<dew_temperature>[^\""]+)\"".*\""precipitation_type\"":\""(?<precipitation_type>[^\""]+)\"".*\""wind_direction_num\"":\""(?<wind_direction_num>[^\""]+)\"".*\""temperature\"":\""(?<temperature>[^\""]+)\"".*\""wind_power\"":\""(?<wind_power>[^\""]+)\""";
        private const string pattern_pm25line = @"data\[""psPm25""\]=(.*?);";
        private const string pattern_featureline = @"data\[""feature""\]=(.*?);";

        /*Small card:
         * Place
         * temperature:temperature
         * description:bodytemp_info
         * temp range:temperature_night、temperature_day
         */
        public string Place { get; set; }
        public string Temperature { get; set; }
        public string Description { get; set; }
        public string TempRange_high { get; set; }
        public string TempRange_low { get; set; }
        public string Weather { get; set; }
        /*Big Card:
         * Place -> choose location
         * temperature
         * description
         * temp range
         * 
         * AQI and its quality 
         * uv index
         * sunset
         * wind and its description
         * feel like
         * precipitation
         * humidity
         * pressure visibility
         */
        public string AQI { get; set; }
        public string AQIQuality { get; set; }
        public string UVIndex { get; set; }
        public string Sunset { get; set; }
        public string Sunrise { get; set; }
        public string WindPower { get; set; }
        public string WindDescription { get; set; }
        public string Precipitation { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string Visibility { get; set; }

        public string getWeatherLine()
        {
            return weatherLine;
        }
        public void MatchLinesInitial()
        {
            MatchFileLine();
            MatchPM25();
            MatchFeatureLine();
        }
        public WeatherAnalysis()
        {
            filePath = WeatherCrawl.filePath;
            inputFileString = File.ReadAllText(filePath);
            if (weatherLine == null) weatherLine = "";
            if (weatherPM25Line == null) weatherPM25Line = "";
            if (weatherFeatureLine == null) weatherFeatureLine = "";
            placeOfChoose = "湖北武汉";
        }
        public WeatherAnalysis(string place)
        {
            this.filePath = WeatherCrawl.filePath;
            inputFileString = File.ReadAllText(filePath);
            if (weatherLine == null) weatherLine = "";
            if (weatherPM25Line == null) weatherPM25Line = "";
            if (weatherFeatureLine == null) weatherFeatureLine = "";
            placeOfChoose = place;
        }
        public string MatchFeatureLine()
        {
            Match match_feature = Regex.Match(inputFileString, pattern_featureline, RegexOptions.Singleline);
            if (match_feature.Success)
            {
                string weatherfeaturetmp = match_feature.Value;
                //Console.WriteLine(weatherDatatmp);
                weatherFeatureLine = Regex.Unescape(weatherfeaturetmp);
                return weatherFeatureLine;
                //Console.WriteLine(weatherLine);
            }
            else
            {
                //Console.WriteLine("");
                return "No match data found of PM2.5.";
            }
        }

        public string MatchFileLine()
        {
            Match match_line = Regex.Match(inputFileString, pattern_dataline, RegexOptions.Singleline);
            if (match_line.Success)
            {
                string weatherDatatmp = match_line.Value;
                //Console.WriteLine(weatherDatatmp);
                weatherLine = Regex.Unescape(weatherDatatmp);
                return weatherLine;
            }
            else
            {
                return "No match data found of txt.";
            }
        }
        private string MatchPM25()
        {
            Match match_pm25 = Regex.Match(inputFileString, pattern_pm25line, RegexOptions.Singleline);
            if (match_pm25.Success)
            {
                string weatherPM25tmp = match_pm25.Value;
                //Console.WriteLine(weatherDatatmp);
                weatherPM25Line = Regex.Unescape(weatherPM25tmp);
                return weatherPM25Line;
                //Console.WriteLine(weatherLine);
            }
            else
            {
                //Console.WriteLine("");
                return "No match data found of PM2.5.";
            }
        }
        public string MatchAirCondition()
        {
            //string input = "data[\"psPm25\"]={\"level\":\"\\u826f\",\"ps_pm25\":\"98\"};";
            string pattern = @"\""level\""\s*:\s*\""(?<level>[^\""]+)\"".*\""ps_pm25\""\s*:\s*\""(?<ps_pm25>[^\""]+)\""";
            Match match = Regex.Match(weatherPM25Line, pattern);
            if (match.Success)
            {
                string level = Regex.Unescape(match.Groups["level"].Value);
                string ps_pm25 = match.Groups["ps_pm25"].Value;
                //Console.WriteLine("level: " + level);
                //Console.WriteLine("ps_pm25: " + ps_pm25);
                string air_result = $"level: {level}\n" +
               $"ps_pm25: {ps_pm25}\n";
                AQIQuality = level;
                AQI = ps_pm25;
                return air_result;
            }
            else
            {
                return "No match data found of AirCondition.";
            }
        }
        public string MatchSunSetAndRise()
        {
            string pattern = @"\""sunriseTime\""\s*:\s*\""(?<sunriseTime>[^\""]+)\"".*\""sunsetTime\""\s*:\s*\""(?<sunsetTime>[^\""]+)\""";
            Match match = Regex.Match(weatherFeatureLine, pattern);
            if (match.Success)
            {
                string sunsetTime = Regex.Unescape(match.Groups["sunsetTime"].Value);
                string sunriseTime = match.Groups["sunriseTime"].Value;
                //Console.WriteLine("level: " + level);
                //Console.WriteLine("ps_pm25: " + ps_pm25);
                string sun_result = $"sunsetTime: {sunsetTime}\n" +
               $"sunriseTime: {sunriseTime}\n";
                Sunrise = sunriseTime;
                Sunset = sunsetTime;
                return sun_result;
            }
            else
            {
                return "No match data found of Sun.";
            }
        }
        /*
        public string MatchWeatherDetail()
        {
            string pattern = @"\""temperature\""\s*:\s*\""(?<temperature>[^\""]+)\""";
            Match match = Regex.Match(weatherLine, pattern);
            if (match.Success)
            {
                string temperature = Regex.Unescape(match.Groups["temperature"].Value);
                return temperature;
            }
            else
            {
                return "No match data found of temperature.";
            }
        }*/
        public Dictionary<string, string> MatchTemperatureDetails(List<string> temperatureKeywords)
        {
            Dictionary<string, string> temperatureMap = new Dictionary<string, string>();

            foreach (string keyword in temperatureKeywords)
            {
                string pattern = $"\\\"{keyword}\\\"\\s*:\\s*\\\"(?<{keyword}>[^\\\"]+)\\\"";
                Match match = Regex.Match(weatherLine, pattern);

                if (match.Success)
                {
                    string temperature = Regex.Unescape(match.Groups[keyword].Value);
                    temperatureMap[keyword] = temperature;
                }
                else
                {
                    temperatureMap[keyword] = $"No match data found of {keyword}.";
                }
            }
            return temperatureMap;
        }
        public async Task<string> getAnalysis()
        {
            WeatherCrawl weatherCrawl = new WeatherCrawl(this.placeOfChoose);
            await weatherCrawl.GenerateTxt();
            MatchLinesInitial();
            MatchAirCondition();//AQI AQIQuality
            MatchSunSetAndRise();//sunrise sunset
            List<string> smallCardKey = new List<string> { "weather", "temperature", "bodytemp_info", "temperature_night", "temperature_day" };
            Dictionary<string, string> smallCardDetails = MatchTemperatureDetails(smallCardKey);
            Place = placeOfChoose;
            Weather = smallCardDetails["weather"];
            Temperature = smallCardDetails["temperature"];
            Description = smallCardDetails["bodytemp_info"];
            TempRange_high = smallCardDetails["temperature_day"];
            TempRange_low = smallCardDetails["temperature_night"];
            
            string tmpText = "";
            foreach (var kvp in smallCardDetails)
            {
                tmpText += ($"{kvp.Key}: {kvp.Value}\n");
            }
            //Console.WriteLine(tmpText);
            //"weather", "temperature", "bodytemp_info", "temperature_night","temperature_day",
            List<string> bigCardKey = new List<string> { "uv_num", "uv", "wind_direction", "wind_power", "precipitation_probability", "humidity", "pressure", "visibility" };
            Dictionary<string, string> bigCardDetails = MatchTemperatureDetails(bigCardKey);
            UVIndex = bigCardDetails["uv_num"];
            WindPower = bigCardDetails["wind_power"];
            WindDescription = bigCardDetails["wind_direction"];
            Precipitation = bigCardDetails["precipitation_probability"];
            Humidity = bigCardDetails["humidity"];
            Pressure = bigCardDetails["pressure"];
            Visibility = bigCardDetails["visibility"];
            return tmpText;
        }
    }
}