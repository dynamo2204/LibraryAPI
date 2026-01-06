using LibraryAPI.Data;
using LibraryAPI.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CopiesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public CopiesController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetCopies()
        {
            return await _context.Copies.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<BookCopy>> PostCopy(BookCopy copy)
        {
            if (!_context.Books.Any(b => b.Id == copy.BookId))
                return BadRequest("Book not found");

            _context.Copies.Add(copy);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCopies), new { id = copy.Id }, copy);
        }
    }
}
