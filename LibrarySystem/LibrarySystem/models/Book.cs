using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.models
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int AvailableCopies { get; set; }
        public string Status { get; set; }  // Available, Borrowed

        public Book(string title, string author, int availableCopies, string status)
        {
            Title = title;
            Author = author;
            AvailableCopies = availableCopies;
            Status = status;
        }

        public override string ToString()
        {
            return $"{Title},{Author},{AvailableCopies},{Status}";
        }
    }
}
