using Books.API.Data;
using Books.API.Models;
using Books.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Books.Tests;

public class BookServiceTests
{
    private readonly DbContextOptions<BookContext> _dbContextOptions;

    public BookServiceTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task AddBookAsync_ShouldAddBook()
    {
        using var context = new BookContext(_dbContextOptions);
        var service = new BookService(context);
        var book = new Book { Title = "New Book" };

        await service.AddBookAsync(book);

        var addedBook = await context.Books.FirstOrDefaultAsync(b => b.Title == "New Book");
        Assert.NotNull(addedBook);
    }

    [Fact]
    public async Task GetAllBooksAsync_ShouldReturnBooks()
    {
        using var context = new BookContext(_dbContextOptions);
        var service = new BookService(context);
        context.Books.Add(new Book { Title = "Book 1" });
        context.Books.Add(new Book { Title = "Book 2" });
        await context.SaveChangesAsync();

        var books = await service.GetAllBooksAsync();

        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook()
    {
        using var context = new BookContext(_dbContextOptions);
        var service = new BookService(context);
        var book = new Book { Title = "Book 1" };
        context.Books.Add(book);
        await context.SaveChangesAsync();

        var foundBook = await service.GetBookByIdAsync(book.Id);

        Assert.NotNull(foundBook);
        Assert.Equal("Book 1", foundBook.Title);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateBook()
    {
        using var context = new BookContext(_dbContextOptions);
        var service = new BookService(context);
        var book = new Book { Title = "Book 1" };
        context.Books.Add(book);
        await context.SaveChangesAsync();

        book.Title = "Updated Book";
        await service.UpdateBookAsync(book);

        var updatedBook = await context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        Assert.Equal("Updated Book", updatedBook.Title);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldDeleteBook()
    {
        using var context = new BookContext(_dbContextOptions);
        var service = new BookService(context);
        var book = new Book { Title = "Book 1" };
        context.Books.Add(book);
        await context.SaveChangesAsync();

        await service.DeleteBookAsync(book.Id);

        var deletedBook = await context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        Assert.Null(deletedBook);
    }
}
