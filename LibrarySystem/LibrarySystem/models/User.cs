using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            BorrowedBooks = new List<Book>();
        }

        public bool CanBorrow()
        {
            return BorrowedBooks.Count < 3;
        }

        public override string ToString()
        {
            return $"{Name},{Email},{string.Join(";", BorrowedBooks.Select(b => b.Title))}";
        }
    }
}
