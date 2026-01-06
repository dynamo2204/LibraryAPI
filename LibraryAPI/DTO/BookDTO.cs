using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryAPI.DTO
{
    public class BookDTO
    {
        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [Required]
        [JsonPropertyName("authorId")]
        public int authorId { get; set; }
    }
}
