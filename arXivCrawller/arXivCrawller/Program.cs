using arXivCrawller;
using ConsoleApp2;
using System;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace ArxivApiParser
{
    //使用例
    class Program
    {
        static void Main(string[] args)
        {
            string query = arXivCategory.Instance().RandomQuery();
            Console.WriteLine(query);
            Console.WriteLine();
            ArxivArticle[] articles = arXivCrawl.ArXivCrawlBySearch(query,3);
            foreach (var article in articles) { //article.Show();
                Console.WriteLine(article.Title);
                Console.WriteLine(article.PublishDate);
                Console.WriteLine();
                Console.WriteLine();
            }
  
            return;
            
            }
        }
    }

