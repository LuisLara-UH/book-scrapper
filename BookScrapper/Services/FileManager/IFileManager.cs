using System;
using System.Collections.Generic;
using BookScrapper.Models;

namespace BookScrapper.Services.FileManager
{
    public interface IFileManager
    {
        IEnumerable<Tuple<int, string>> ReadInputFile(string inputFile); // Returns <row, bookId> from input file
        void WriteOutputFile(string outputFile, List<IBook> books);
    }
}