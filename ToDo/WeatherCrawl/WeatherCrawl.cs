using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;


namespace UIDisplay.WeatherCrawl
{
    public class WeatherCrawl
    {
        public static string filePath = "../weather.txt";
        private string placeName;
        private async Task<string> DownloadHtmlAsync(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return await client.DownloadStringTaskAsync(url);
            }
        }
        private bool IsHtml(string html)
        {
            string pattern = @"<\s*html\s*>";
            return Regex.IsMatch(html, pattern, RegexOptions.IgnoreCase);
        }
        private string urlGen()
        {
            string keyword = this.placeName+ "天气";
            string encodedKeyword = WebUtility.UrlEncode(keyword);
            string url = "https://weathernew.pae.baidu.com/weathernew/pc?query=" + encodedKeyword + "&srcid=4982&forecast=long_day_forecast";
            //Console.WriteLine(url);
            return url;
        }
        public WeatherCrawl(string place)
        {

            this.placeName = place;
        }
        public WeatherCrawl()
        {
            this.placeName = "湖北武汉";
                //"https://weathernew.pae.baidu.com/weathernew/pc?query=%E6%B9%96%E5%8C%97%E6%AD%A6%E6%B1%89%E5%A4%A9%E6%B0%94&srcid=4982&forecast=long_day_forecast";
        }
       // await CrawlAsync(url);
       // Console.WriteLine(url);
        private async Task<string> CrawlAsync(string url)
        {
            string html = await DownloadHtmlAsync(url);
            //Console.WriteLine(html);
            //Console.WriteLine(url);
            return html;
        }
        public async Task GenerateTxt()
        {
            string url = this.urlGen();
            string html = await CrawlAsync(url);
            //Console.WriteLine(html);
            File.WriteAllText(filePath, string.Empty);
            File.WriteAllText(filePath, html);
        }
    }
}
