using System.Collections.Generic;
using System;


/// <summary>
/// 表示 ArXiv 上的文章信息。
/// </summary>
public class ArxivArticle
{
    /// <summary>
 /// 获取或设置文章的标题。
 /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 获取或设置文章的作者列表。
    /// </summary>
    public List<string> Authors { get; set; }

    /// <summary>
    /// 获取或设置文章的摘要。
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 获取或设置文章的类别列表。
    /// </summary>
    public List<string> Categories { get; set; }

    /// <summary>
    /// 获取或设置文章的主页链接。
    /// </summary>
    public string Homepage { get; set; }

    /// <summary>
    /// 获取或设置文章的 PDF 页面链接。
    /// </summary>
    public string Pdfpage { get; set; }

    /// <summary>
    /// 获取或设置文章的 DOI 页面链接。
    /// </summary>
    public string Doipage { get; set; }

    /// <summary>
    /// 获取或设置文章的发布日期。
    /// </summary>
    public string PublishDate { get; set; }

    /// <summary>
    /// 获取或设置文章的更新日期。
    /// </summary>
    public string UpdateDate { get; set; }

    /// <summary>
    /// 获取或设置文章的发布信息。
    /// </summary>
    public string Publish { get; set; }

    /// <summary>
    /// 控制台打印文章信息。
    /// </summary>
    public void Show()
    {
        Console.WriteLine(Title);
        Console.WriteLine("Authors: ");
        foreach (var author in Authors)
        {
            Console.Write(author + " ");
        }
        Console.WriteLine("\n" + Summary);
        Console.WriteLine("Categories: ");
        foreach (var category in Categories) { Console.Write(category + " "); }
        Console.WriteLine(Homepage);
        Console.WriteLine(Pdfpage);
        Console.WriteLine(PublishDate);
        Console.WriteLine(UpdateDate);
        Console.WriteLine();
        Console.WriteLine();
    }
}
