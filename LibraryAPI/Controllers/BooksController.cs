using LibraryAPI.Data;
using LibraryAPI.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] int? authorId)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(long id)
        {
            if (id > int.MaxValue || id < int.MinValue) return NotFound();
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == (int)id);

            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDto)
        {
            var authorsExists = await _context.Authors.AnyAsync(a => a.Id == bookDto.authorId);
            if (!authorsExists)
            {
                return BadRequest("Autor nie istnieje");
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Year = bookDto.Year,
                AuthorId = bookDto.authorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            await _context.Entry(book).Reference(b => b.Author).LoadAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(long id, BookDTO bookDto)
        {
            if (id > int.MaxValue || id < int.MinValue) return NotFound();
            var book = await _context.Books.FindAsync((int)id);
            if (book == null) return NotFound();

            var authorsExists = await _context.Authors.AnyAsync(a => a.Id == bookDto.authorId);
            if (!authorsExists) return BadRequest("authors does not exist.");

            book.Title = bookDto.Title;
            book.Year = bookDto.Year;
            book.AuthorId = bookDto.authorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            if (id > int.MaxValue || id < int.MinValue) return NotFound();
            var book = await _context.Books.FindAsync((int)id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
