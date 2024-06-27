using Books.API.Models;

namespace Books.API.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task AddBookAsync(Book book);
    Task<bool> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
}