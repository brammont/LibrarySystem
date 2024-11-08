using System;
using Xunit;
using LibrarySystem.Models;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class LibraryTests
    {
        private LibraryManager libraryManager;
        private User user;
        private Book availableBook;
        private Book borrowedBook;

        public LibraryTests()
        {
            // Initialize resources before each test
            libraryManager = new LibraryManager();
            user = new User("TestUser", "testuser@example.com");

            availableBook = new Book("Available Book", "Author", 1, "Available");
            borrowedBook = new Book("Borrowed Book", "Author", 1, "Borrowed");

            // Manually add test data (since files may not exist for testing)
            libraryManager.Books = new List<Book> { availableBook, borrowedBook };
            libraryManager.Users = new List<User> { user };
        }

        [Fact]
        public void TestBookSearchByTitle_ShouldReturnCorrectBook()
        {
            // Act
            var result = libraryManager.Books.Find(b => b.Title == "Available Book");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Available Book", result.Title);
        }

        [Fact]
        public void TestBorrowBook_AvailableBook_ShouldSucceed()
        {
            // Act
            string result = libraryManager.BorrowBook("Available Book", "TestUser");

            // Assert
            Assert.Equal("Book borrowed successfully.", result);
            Assert.Equal("Borrowed", availableBook.Status);
            Assert.Contains(availableBook, user.BorrowedBooks);
        }

        [Fact]
        public void TestBorrowBook_ExceedingLimit_ShouldFail()
        {
            // Arrange
            user.BorrowedBooks.Add(new Book("Book 1", "Author", 1, "Borrowed"));
            user.BorrowedBooks.Add(new Book("Book 2", "Author", 1, "Borrowed"));
            user.BorrowedBooks.Add(new Book("Book 3", "Author", 1, "Borrowed"));

            // Act
            string result = libraryManager.BorrowBook("Available Book", "TestUser");

            // Assert
            Assert.Equal("User cannot borrow more than 3 books.", result);
        }

        [Fact]
        public void TestBorrowBook_BookNotAvailable_ShouldFail()
        {
            // Act
            string result = libraryManager.BorrowBook("Borrowed Book", "TestUser");

            // Assert
            Assert.Equal("Book is not available.", result);
        }

        [Fact]
        public void TestReturnBook_ShouldSucceed()
        {
            // Arrange
            libraryManager.BorrowBook("Available Book", "TestUser");

            // Act
            string result = libraryManager.ReturnBook("Available Book", "TestUser");

            // Assert
            Assert.Equal("Book returned successfully.", result);
            Assert.Equal("Available", availableBook.Status);
            Assert.DoesNotContain(availableBook, user.BorrowedBooks);
        }

        [Fact]
        public void TestReturnBook_BookOrUserNotFound_ShouldFail()
        {
            // Act
            string result = libraryManager.ReturnBook("Nonexistent Book", "Nonexistent User");

            // Assert
            Assert.Equal("Book or User not found.", result);
        }
    }
}
