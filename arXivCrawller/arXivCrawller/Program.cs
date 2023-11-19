using ConsoleApp2;
using System;
using System.Linq;
using System.Xml.Linq;

namespace ArxivApiParser
{
    //使用例
    class Program
    {
        static void  Main(string[] args)
        {
            ArxivArticle[] articles = arXivCrawl.ArXivCrawlBySearch("all:LLM",10);
            foreach (var article in articles) { //article.Show();
                Console.WriteLine(article.Title);
                Console.WriteLine(article.PublishDate);
                //Console.WriteLine(article.UpdateDate);
                Console.WriteLine();
                Console.WriteLine();
            }
  
            return;
        }
    }
}
