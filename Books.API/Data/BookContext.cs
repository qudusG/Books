using Books.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Data;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
}