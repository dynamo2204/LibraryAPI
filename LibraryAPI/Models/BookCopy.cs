using System.Text.Json.Serialization;

namespace LibraryAPI.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
