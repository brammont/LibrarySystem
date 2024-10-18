using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LibrarySystem.models
{
    class LibraryManager
    {
        private string booksFile = "books.csv";
        private string usersFile = "users.csv";
        public List<Book> Books { get; private set; }
        public List<User> Users { get; private set; }

        public LibraryManager()
        {
            Books = LoadBooks();
            Users = LoadUsers();
        }

        // Load books from CSV file
        private List<Book> LoadBooks()
        {
            var books = new List<Book>();
            if (File.Exists(booksFile))
            {
                var lines = File.ReadAllLines(booksFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var book = new Book(parts[0], parts[1], int.Parse(parts[2]), parts[3]);
                    books.Add(book);
                }
            }
            return books;
        }

        // Load users from CSV file
        private List<User> LoadUsers()
        {
            var users = new List<User>();
            if (File.Exists(usersFile))
            {
                var lines = File.ReadAllLines(usersFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var user = new User(parts[0], parts[1]);
                    users.Add(user);
                }
            }
            return users;
        }

        // Save books to CSV
        public void SaveBooks()
        {
            File.WriteAllLines(booksFile, Books.Select(b => b.ToString()));
        }

        // Save users to CSV
        public void SaveUsers()
        {
            File.WriteAllLines(usersFile, Users.Select(u => u.ToString()));
        }

        // Borrow a book
        public string BorrowBook(string bookTitle, string userName)
        {
            var user = Users.FirstOrDefault(u => u.Name == userName);
            var book = Books.FirstOrDefault(b => b.Title == bookTitle && b.Status == "Available");

            if (user == null)
                return "User not found.";
            if (book == null)
                return "Book is not available.";

            if (!user.CanBorrow())
                return "User cannot borrow more than 3 books.";

            book.Status = "Borrowed";
            user.BorrowedBooks.Add(book);
            SaveBooks();
            SaveUsers();

            return "Book borrowed successfully.";
        }

        // Return a book
        public string ReturnBook(string bookTitle, string userName)
        {
            var user = Users.FirstOrDefault(u => u.Name == userName);
            var book = Books.FirstOrDefault(b => b.Title == bookTitle && b.Status == "Borrowed");

            if (user == null || book == null)
                return "Book or User not found.";

            book.Status = "Available";
            user.BorrowedBooks.Remove(book);
            SaveBooks();
            SaveUsers();

            return "Book returned successfully.";
        }

    }
}
