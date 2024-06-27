using Books.API.Data;
using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Services;

public class BookService : IBookService
{
    private readonly BookContext _context;

    public BookService(BookContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(book.Id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return false;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return true;
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}