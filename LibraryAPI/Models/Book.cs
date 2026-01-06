using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(0, int.MaxValue)]
        public int Year { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [JsonIgnore]
        public List<BookCopy> Copies { get; set; } = new List<BookCopy>();
    }
}
