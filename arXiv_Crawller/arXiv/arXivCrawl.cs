using arXiv_Crawller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace arXiv_Crawller
{
    /// <summary>
    /// 提供 arXiv 文章获取相关功能的类。
    /// </summary>
    public class arXivCrawl
    {
        /// <summary>
        /// 获取一个随机的 arXiv 文章。
        /// </summary>
        /// <returns>随机的 arXiv 文章。</returns>
        public static ArxivArticle GetOneRandomArticle()
        {
            string query = arXivCategory.RandomQuery();

            ArxivArticle[] articles = arXivCrawl.ArXivCrawlBySearch(query, 1);

            return articles[0];
        }

        /// <summary>
        /// 根据搜索条件获取 arXiv 文章。
        /// </summary>
        /// <param name="query">搜索条件。</param>
        /// <param name="maxResult">最大结果数。</param>
        /// <returns>获取到的 arXiv 文章数组。</returns>
        public static ArxivArticle[] ArXivCrawlBySearch(string query, int maxResult)
        {
            string apiUrl = $"http://export.arxiv.org/api/query?search_query={query}&max_results={maxResult.ToString()}&sortBy=lastUpdatedDate&sortOrder=descending";

            XDocument doc = XDocument.Load(apiUrl);

            // 声明命名空间
            XNamespace atom = "http://www.w3.org/2005/Atom";
            XNamespace arxiv = "http://arxiv.org/schemas/atom";

            // 查询文章条目
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
            // 存储结果为arXivArticle对象
            foreach (var entry in entries)
            {
                ArxivArticle article = new ArxivArticle();
                article.Title = entry.Title;
                article.Authors = entry.Authors.ToList();
                article.Summary = entry.Summary;
                article.Categories = entry.Categories.ToList();
                foreach (var link in entry.Links)
                {
                    switch (link.Title)
                    {
                        case null: article.Homepage = link.Url; break;
                        case "pdf": article.Pdfpage = link.Url; break;
                        case "doi": article.Doipage = link.Url; break;
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