using Xunit;
using LibrarySystem;
using System;
using System.Collections.Generic;
using LibrarySystem.Models;

namespace TestLibrarySystem
{
    public class UnitTest1
    {
        [Fact]
        public void TestBorrowBook_LimitNotExceeded_Success()
        {
            // Arrange
            var user = new User("John Doe", "johndoe@example.com")
            {
                BorrowedBooks = new List<Book> { new Book("Existing Book 1", "Author A", 1, "Available"), new Book("Existing Book 2", "Author B", 1, "Available") }
            };
            var book = new Book("New Book", "Author C", 1, "Available");

            // Act
            var result = user.BorrowBook(book);

            // Assert
            Assert.True(result, "Book should be borrowed successfully as limit not exceeded.");
            Assert.Equal("Borrowed", book.Status);
        }

        [Fact]
        public void TestBorrowBook_LimitExceeded_Fail()
        {
            // Arrange
            var user = new User("Jane Smith", "janesmith@example.com")
            {
                BorrowedBooks = new List<Book> { new Book("Book 1", "Author A", 1, "Borrowed"), new Book("Book 2", "Author B", 1, "Borrowed"), new Book("Book 3", "Author C", 1, "Borrowed") }
            };
            var book = new Book("Exceeding Book", "Author D", 1, "Available");

            // Act
            var result = user.BorrowBook(book);

            // Assert
            Assert.False(result, "Book should not be borrowed as user has reached borrow limit.");
        }

        [Fact]
        public void TestIsAvailable_WhenBookIsAvailable_ReturnsTrue()
        {
            // Arrange
            var book = new Book("Available Book", "Author E", 1, "Available");

            // Act
            var isAvailable = book.IsAvailable();

            // Assert
            Assert.True(isAvailable, "Book should be available for borrowing.");
        }

        [Fact]
        public void TestIsAvailable_WhenBookIsNotAvailable_ReturnsFalse()
        {
            // Arrange
            var book = new Book("Unavailable Book", "Author F", 0, "Borrowed");

            // Act
            var isAvailable = book.IsAvailable();

            // Assert
            Assert.False(isAvailable, "Book should not be available for borrowing.");
        }
    }
}
