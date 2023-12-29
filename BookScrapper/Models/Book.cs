namespace BookScrapper.Models
{
    public enum DataRetrievalType
    {
        Server = 1,
        Cache = 2
    }
    
    public class Book: IBook
    {
        public int RowNumber { get; set; }
        public DataRetrievalType RetrievalType { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string AuthorNames { get; set; }
        public int? NumberOfPages { get; set; }
        public string PublishDate { get; set; }

        public int GetRowNumber() => RowNumber;

        public DataRetrievalType GetRetrievalType() => RetrievalType;

        public string GetISBN() => ISBN;

        public string GetTitle() => Title;

        public string GetSubtitle() => Subtitle;

        public string GetAuthorNames() => AuthorNames;

        public int? GetNumberOfPages() => NumberOfPages;

        public string GetPublishDate() => PublishDate;
    }
}