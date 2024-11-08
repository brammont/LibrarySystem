using Xunit;
using LibrarySystem;
using System;
using System.Collections.Generic;
using System.IO;
using LibrarySystem.Models;

namespace TestLibrarySystem
{
    public class UnitTest1
    {
        private readonly string resultFilePath = "TestResults.txt";

        private void WriteResult(string testName, bool passed, string message = "")
        {
            string result = $"{DateTime.Now}: {testName} - {(passed ? "Passed" : "Failed")} {message}";
            File.AppendAllText(resultFilePath, result + Environment.NewLine);
        }

        [Fact]
        public void TestBorrowBook_LimitNotExceeded_Success()
        {
            // Arrange
            var user = new User("John Doe", "johndoe@example.com")
            {
                BorrowedBooks = new List<Book> { new Book("Existing Book 1", "Author A", 1, "Available"), new Book("Existing Book 2", "Author B", 1, "Available") }
            };
            var book = new Book("New Book", "Author C", 1, "Available");

            try
            {
                // Act
                var result = user.BorrowBook(book);

                // Assert
                Assert.True(result, "Book should be borrowed successfully as limit not exceeded.");
                Assert.Equal("Borrowed", book.Status);

                // Write result to file
                WriteResult(nameof(TestBorrowBook_LimitNotExceeded_Success), true);
            }
            catch (Exception ex)
            {
                WriteResult(nameof(TestBorrowBook_LimitNotExceeded_Success), false, ex.Message);
                throw;
            }
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

            try
            {
                // Act
                var result = user.BorrowBook(book);

                // Assert
                Assert.False(result, "Book should not be borrowed as user has reached borrow limit.");

                // Write result to file
                WriteResult(nameof(TestBorrowBook_LimitExceeded_Fail), true);
            }
            catch (Exception ex)
            {
                WriteResult(nameof(TestBorrowBook_LimitExceeded_Fail), false, ex.Message);
                throw;
            }
        }

        [Fact]
        public void TestIsAvailable_WhenBookIsAvailable_ReturnsTrue()
        {
            // Arrange
            var book = new Book("Available Book", "Author E", 1, "Available");

            try
            {
                // Act
                var isAvailable = book.IsAvailable();

                // Assert
                Assert.True(isAvailable, "Book should be available for borrowing.");

                // Write result to file
                WriteResult(nameof(TestIsAvailable_WhenBookIsAvailable_ReturnsTrue), true);
            }
            catch (Exception ex)
            {
                WriteResult(nameof(TestIsAvailable_WhenBookIsAvailable_ReturnsTrue), false, ex.Message);
                throw;
            }
        }

        [Fact]
        public void TestIsAvailable_WhenBookIsNotAvailable_ReturnsFalse()
        {
            // Arrange
            var book = new Book("Unavailable Book", "Author F", 0, "Borrowed");

            try
            {
                // Act
                var isAvailable = book.IsAvailable();

                // Assert
                Assert.False(isAvailable, "Book should not be available for borrowing.");

                // Write result to file
                WriteResult(nameof(TestIsAvailable_WhenBookIsNotAvailable_ReturnsFalse), true);
            }
            catch (Exception ex)
            {
                WriteResult(nameof(TestIsAvailable_WhenBookIsNotAvailable_ReturnsFalse), false, ex.Message);
                throw;
            }
        }
    }
}
