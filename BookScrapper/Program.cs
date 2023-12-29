using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BookScrapper.Services.BookManager;
using BookScrapper.Services.FileManager;

namespace BookScrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "./input.txt";
            string outputFile = "./output.csv";

            IFileManager fileManager = new FileManager();
            IBookManager bookManager = new BookManager();
            
            bookManager.GetBooksFromISBN(inputFile, fileManager);
            bookManager.WriteBooksInfo(outputFile, fileManager);

            Console.WriteLine("Processing completed. Results written to " + outputFile);
        }
    }
}