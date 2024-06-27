using Books.API.Models;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(await _bookService.GetAllBooksAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        var result = await _bookService.UpdateBookAsync(book);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
    {
        await _bookService.AddBookAsync(book);

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
