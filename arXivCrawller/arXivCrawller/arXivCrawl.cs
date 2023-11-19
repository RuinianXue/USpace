using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp2
{
    internal class arXivCrawl
    {
        public static ArxivArticle[] ArXivCrawlBySearch(string query, int maxResult){

            string apiUrl = $"http://export.arxiv.org/api/query?search_query={query}&max_results={maxResult.ToString()}";
            
            //测试完毕后删除
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string xmlContent = response.Content.ReadAsStringAsync().Result;
                    // 解析 XML 内容以提取相关信息。
                    // 根据需要处理数据。
                    Console.WriteLine(xmlContent);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            //测试完毕后删除
            

            XDocument doc = XDocument.Load(apiUrl);
            // Declare the xml namespaces
            XNamespace atom = "http://www.w3.org/2005/Atom";
            XNamespace arxiv = "http://arxiv.org/schemas/atom";

            // Query the entries
            var entries = from entry in doc.Descendants(atom + "entry")
                          select new
                          {
                              Title = entry.Element(atom + "title").Value,
                              Authors = from author in entry.Elements(atom + "author")
                                        select author.Element(atom + "name").Value,
                              Summary = entry.Element(atom + "summary").Value,
                              Categories = from category in entry.Elements(atom + "category")
                                           select category.Attribute("term").Value,
                              Links = from link in entry.Elements(atom + "link")
                                      select new
                                      {
                                          Title = link.Attribute("title")?.Value,
                                          Url = link.Attribute("href").Value
                                      },
                              Published = entry.Element(atom + "published")?.Value,
                              Updated = entry.Element(atom + "updated")?.Value
                          };

            List<ArxivArticle> articles = new List<ArxivArticle>();
            // Print the results
            foreach (var entry in entries)
            {
                ArxivArticle article = new ArxivArticle();
                article.Title = entry.Title;
                article.Authors = entry.Authors.ToList();
                article.Summary = entry.Summary;
                article.Categories = entry.Categories.ToList();
                foreach(var link in entry.Links)
                {
                    switch (link.Title)
                    {
                        case null : article.Homepage = link.Url;break;
                        case "pdf" : article.Pdfpage = link.Url;break;
                        case "doi" : article.Doipage = link.Url;break;
                    }
                }
                article.PublishDate = entry.Published;
                article.UpdateDate = entry.Updated;
                articles.Add(article);
            }
            
            return articles.ToArray();
        }

    }
}
