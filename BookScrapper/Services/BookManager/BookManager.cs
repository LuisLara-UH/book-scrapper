using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BookScrapper.Models;
using BookScrapper.Services.FileManager;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace BookScrapper.Services.BookManager
{
    public class BookManager : IBookManager
    {
        private List<IBook> _books;

        public BookManager()
        {
            _books = new List<IBook>();
        }

        public async void GetBooksFromISBN(string inputFile, IFileManager fileManager)
        {
            foreach (var isbnNumber in fileManager.ReadInputFile(inputFile))
            {
                int row = isbnNumber.Item1;
                string isbn = isbnNumber.Item2;
                
                _books.Add(await GetBookInfo(row, isbn));
            }
        }
        
        async Task<IBook> GetBookInfo(int rowNumber, string isbn)
        {
            string apiUrl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&jscmd=data&format=json";

            using (var client = new HttpClient())
            {
                bool isDataInCache = _books.Exists(book => book.GetISBN() == isbn);
                if (isDataInCache)
                {
                    var data = _books.Find(book => book.GetISBN() == isbn);
                    return new Book
                    {
                        RowNumber = rowNumber,
                        RetrievalType = DataRetrievalType.Cache,
                        ISBN = isbn,
                        Title = data.GetTitle(),
                        Subtitle = data.GetSubtitle(),
                        AuthorNames = data.GetAuthorNames(),
                        NumberOfPages = data.GetNumberOfPages(),
                        PublishDate = data.GetPublishDate()
                    };
                }

                // Retrieve data from the API
                byte[] response = client.GetByteArrayAsync(apiUrl).Result;
                string responseBody = Encoding.UTF8.GetString(response);

                dynamic jsonData = JsonConvert.DeserializeObject(responseBody);
                dynamic bookData = jsonData[$"ISBN:{isbn}"];

                return new Book
                {
                    RowNumber = rowNumber,
                    RetrievalType = DataRetrievalType.Server,
                    ISBN = isbn,
                    Title = bookData?.title,
                    Subtitle = bookData?.subtitle,
                    AuthorNames = GetAuthorNames(bookData?.authors),
                    NumberOfPages = bookData?.number_of_pages,
                    PublishDate = bookData?.publish_date
                };
            }
        }
        
        static string GetAuthorNames(dynamic authors)
        {
            if (authors == null)
            {
                return null;
            }

            List<string> authorNames = new List<string>();

            foreach (dynamic author in authors)
            {
                if (author != null && author.name != null)
                {
                    authorNames.Add(author.name.ToString());
                }
            }

            return string.Join("; ", authorNames);
        }


        public void WriteBooksInfo(string outputFile, IFileManager fileManager)
        {
            fileManager.WriteOutputFile(outputFile, _books);
        }
    }
}