using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Book
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
        public bool IsAvailable()
        {
            return Status == "Available" && AvailableCopies > 0;
        }
        public override string ToString()
        {
            return $"{Title},{Author},{AvailableCopies},{Status}";
        }
    }
}
