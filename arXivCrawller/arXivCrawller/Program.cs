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
            ArxivArticle[] articles = arXivCrawl.ArXivCrawlBySearch("all:machine",10);
            foreach (var article in articles) { article.Show(); }

            return;
        }
    }
}
