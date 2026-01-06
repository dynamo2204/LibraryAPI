using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonIgnore]
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
