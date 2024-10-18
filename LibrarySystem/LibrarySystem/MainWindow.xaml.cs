using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibrarySystem.Models;  // Reference the namespace where the classes reside



namespace LibrarySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // File paths for .csv storage
        private const string BooksFile = "books.csv";
        private const string BorrowedFile = "borrowed.csv";

        // List to hold available and borrowed books
        private List<string> availableBooks = new List<string>();
        private List<string> borrowedBooks = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
            DisplayAvailableBooks();
        }
        // Load books from CSV file
        private void LoadBooks()
        {
            if (File.Exists(BooksFile))
            {
                availableBooks = new List<string>(File.ReadAllLines(BooksFile));
            }

            if (File.Exists(BorrowedFile))
            {
                borrowedBooks = new List<string>(File.ReadAllLines(BorrowedFile));
            }
        }
        // Save updated lists to CSV files
        private void SaveToCSV(string filePath, List<string> data)
        {
            File.WriteAllLines(filePath, data);
        }

        // Display available books in the ListBox
        private void DisplayAvailableBooks()
        {
            BookList.Items.Clear();
            BorrowedBookList.Items.Clear();
            foreach (var book in availableBooks)
            {
                BookList.Items.Add(book);
            }
            foreach (var borrowedBook in borrowedBooks)
            {
                string[] words = borrowedBook.Split(',');
                BorrowedBookList.Items.Add("User:   " + words[0]+"   Book:  " + words[1]);
            }
        }
        private void BorrowBook_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName.Text.Trim();
            string bookTitle = BookTitle.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(bookTitle))
            {
                StatusMessage.Text = "Please enter both user name and book title.";
                return;
            }

            if (availableBooks.Contains(bookTitle))
            {
                // Borrow the book
                availableBooks.Remove(bookTitle);
                borrowedBooks.Add($"{userName},{bookTitle}");

                // Update CSV
                SaveToCSV(BorrowedFile, borrowedBooks);
                SaveToCSV(BooksFile, availableBooks);

                // Update UI
                DisplayAvailableBooks();
                StatusMessage.Text = $"{userName} borrowed '{bookTitle}' successfully.";
            }
            else
            {
                StatusMessage.Text = $"'{bookTitle}' is not available.";
            }
        }

        private void ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName.Text.Trim();
            string bookTitle = BookTitle.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(bookTitle))
            {
                StatusMessage.Text = "Please enter both user name and book title.";
                return;
            }

            string borrowedRecord = $"{userName},{bookTitle}";
            if (borrowedBooks.Contains(borrowedRecord))
            {
                // Return the book
                borrowedBooks.Remove(borrowedRecord);
                availableBooks.Add(bookTitle);

                // Update CSV
                SaveToCSV(BorrowedFile, borrowedBooks);
                SaveToCSV(BooksFile, availableBooks);

                // Update UI
                DisplayAvailableBooks();
                StatusMessage.Text = $"{userName} returned '{bookTitle}' successfully.";
            }
            else
            {
                StatusMessage.Text = $"No record of '{bookTitle}' being borrowed by {userName}.";
            }
        }
    }
}