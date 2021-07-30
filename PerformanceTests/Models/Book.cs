namespace PerformanceTests.Models
{
    public class Book
    {
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public SimpleDelegate SimpleDelegate { get; set; }
        public string Title { get; set; }

        public Book()
        {
            Author = string.Empty;
            Title = string.Empty;
            NumberOfPages = 100;
        }
    }

    public delegate void SimpleDelegate();
}