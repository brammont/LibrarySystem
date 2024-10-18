using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemTests
{
    [TestClass]
    public class LibraryManagerTests
    {
        private LibraryManager _libraryManager;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the LibraryManager before each test
            _libraryManager = new LibraryManager();
        }

        [TestMethod]
        public void BorrowBook_ShouldUpdateBookStatusCorrectly()
        {
            // Arrange
            var userName = "John Doe";
            var bookTitle = "The Great Gatsby";

            // Act
            var result = _libraryManager.BorrowBook(userName, bookTitle);

            // Assert
            Assert.IsTrue(result, "The user should be able to borrow an available book.");
        }

        [TestMethod]
        public void ReturnBook_ShouldUpdateBookStatusCorrectly()
        {
            // Arrange
            var userName = "Jane Smith";
            var bookTitle = "The Alchemist";
            _libraryManager.BorrowBook(userName, bookTitle);

            // Act
            var result = _libraryManager.ReturnBook(userName, bookTitle);

            // Assert
            Assert.IsTrue(result, "The user should be able to return a borrowed book.");
        }

        [TestMethod]
        public void AvailableBooks_ShouldReturnCorrectBooks()
        {
            // Act
            var availableBooks = _libraryManager.GetAvailableBooks();

            // Assert
            Assert.IsTrue(availableBooks.Contains("1984"), "1984 should be in the available books list.");
        }

        [TestMethod]
        public void BorrowBook_ShouldNotAllowIfAlreadyBorrowed()
        {
            // Arrange
            var userName = "Mike Johnson";
            var bookTitle = "To Kill a Mockingbird";
            _libraryManager.BorrowBook(userName, bookTitle);

            // Act
            var secondBorrowAttempt = _libraryManager.BorrowBook("Emily Davis", bookTitle);

            // Assert
            Assert.IsFalse(secondBorrowAttempt, "The second user should not be able to borrow a book already borrowed.");
        }
    }
}
