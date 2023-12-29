namespace BookScrapper.Models
{
    public interface IBook
    {
        int GetRowNumber();
        DataRetrievalType GetRetrievalType();
        string GetISBN();
        string GetTitle();
        string GetSubtitle();
        string GetAuthorNames();
        int? GetNumberOfPages();
        string GetPublishDate();
    }
}