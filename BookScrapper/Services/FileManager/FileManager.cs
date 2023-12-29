using System;
using System.Collections.Generic;
using BookScrapper.Models;
using System.IO;
using System.Text;

namespace BookScrapper.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public IEnumerable<Tuple<int, string>> ReadInputFile(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            int rowNumber = 1;

            foreach (string line in lines)
            {
                string[] isbns = line.Split(',');

                foreach (string isbn in isbns)
                {
                    string formattedISBN = isbn.Trim();
                    yield return new Tuple<int, string>(rowNumber, formattedISBN);
                }
                rowNumber++;
            }
        }

        public void WriteOutputFile(string outputFile, List<IBook> books)
        {
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine("Row Number,Data Retrieval Type,ISBN,Title,Subtitle,Author Names,Number of Pages,Publish Date");

                foreach (var book in books)
                {
                    writer.WriteLine($"{book.GetRowNumber()},{book.GetRetrievalType()},\"{book.GetISBN()}\"," +
                                     $"\"{book.GetTitle()}\",\"{book.GetSubtitle()}\",\"{book.GetAuthorNames()}\"," +
                                     $"{book.GetNumberOfPages()},\"{book.GetPublishDate()}\"");
                }
            }
        }

    }
}