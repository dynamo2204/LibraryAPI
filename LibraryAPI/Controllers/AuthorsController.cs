using LibraryAPI.Data;
using LibraryAPI.DTO;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(long id)
        {
            if (id > int.MaxValue || id < int.MinValue)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync((int)id);
            if (author == null) return NotFound();
            return author;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorsDTO authorsDTO)
        {
            var author = new Author
            {
                FirstName = authorsDTO.FirstName,
                LastName = authorsDTO.LastName
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorsDTO authorsDTO)
        {
            if (id > int.MaxValue || id < int.MinValue)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync((int)id);
            if (author == null) return NotFound();

            author.FirstName = authorsDTO.FirstName;
            author.LastName = authorsDTO.LastName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (id > int.MaxValue || id < int.MinValue)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync((int)id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


