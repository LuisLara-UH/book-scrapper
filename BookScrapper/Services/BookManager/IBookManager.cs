using BookScrapper.Services.FileManager;

namespace BookScrapper.Services.BookManager
{
    public interface IBookManager
    {
        void GetBooksFromISBN(string inputFile, IFileManager fileManager);
        void WriteBooksInfo(string outputFile, IFileManager fileManager);
    }
}