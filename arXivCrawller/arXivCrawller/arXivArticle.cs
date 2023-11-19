public class ArxivArticle
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string Summary { get; set; }
    public List<string> Categories { get; set; }

    public string Homepage { get; set; }
    public string Pdfpage { get; set; }
    public string Doipage { get; set; }


    public string PublishDate {  get; set; }
    public string UpdateDate {  get; set; }
    public string Publish {  get; set; }

    public void Show()
    {
        Console.WriteLine(Title);
        Console.WriteLine("Authors: ");
        foreach(var author in Authors)
        {
            Console.Write(author + " ");
        }
        Console.WriteLine("\n" + Summary);
        Console.WriteLine("Categories: ");
        foreach(var category in Categories) { Console.Write(category + " "); }
        Console.WriteLine(Homepage);
        Console.WriteLine(Pdfpage);
        Console.WriteLine(PublishDate);
        Console.WriteLine(UpdateDate);
        Console.WriteLine();
    }
}
