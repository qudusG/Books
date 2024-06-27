using Books.API.Controllers;
using Books.API.Models;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Books.Tests;

public class BookControllerTests
{
    private readonly Mock<IBookService> _mockBookService;
    private readonly BooksController _controller;

    public BookControllerTests()
    {
        _mockBookService = new Mock<IBookService>();
        _controller = new BooksController(_mockBookService.Object);
    }

    [Fact]
    public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
    {
        // Arrange
        var books = new List<Book> { new() { Id = 1, Title = "Test Book" } };
        _mockBookService.Setup(s => s.GetAllBooksAsync()).ReturnsAsync(books);

        // Act
        var result = await _controller.GetBooks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnBooks = Assert.IsType<List<Book>>(okResult.Value);
        Assert.Single(returnBooks);
    }

    [Fact]
    public async Task GetBookById_ReturnsOkResult_WithBook()
    {
        var book = new Book { Id = 1, Title = "Test Book" };
        _mockBookService.Setup(s => s.GetBookByIdAsync(1)).ReturnsAsync(book);

        var result = await _controller.GetBook(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnBook = Assert.IsType<Book>(okResult.Value);
        Assert.Equal(1, returnBook.Id);
    }

    [Fact]
    public async Task UpdateBook_ReturnsNoContentResult()
    {
        var book = new Book { Id = 1, Title = "Updated Book" };
        _mockBookService.Setup(s => s.UpdateBookAsync(book)).ReturnsAsync(true);

        var result = await _controller.PutBook(1, book);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBook_ReturnsNoContentResult()
    {
        var book = new Book { Id = 1, Title = "Test Book" };
        _mockBookService.Setup(s => s.GetBookByIdAsync(1)).ReturnsAsync(book);
        _mockBookService.Setup(s => s.DeleteBookAsync(book.Id)).ReturnsAsync(true);

        var result = await _controller.DeleteBook(1);

        Assert.IsType<NoContentResult>(result);
    }
}
