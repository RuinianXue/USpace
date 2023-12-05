using arXivCrawller;
using arXivCrawller;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

//使用例
class Program
{


    static async Task Main(string[] args)
    {
        string query = arXivCategory.Instance().RandomQuery();
        Console.WriteLine(query);
        Console.WriteLine();
        ArxivArticle[] articles = arXivCrawl.ArXivCrawlBySearch(query, 3);
        foreach (var article in articles)
        { //article.Show();
            Console.WriteLine(article.Title);
            Console.WriteLine(article.PublishDate);
            Console.WriteLine();
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();


    }
}