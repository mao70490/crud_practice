using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CrudPractice1.Enums;

namespace CrudPractice1.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string? Subtitle { get; set; }

        [MaxLength(255)]
        public string Author { get; set; }

        [MaxLength(255)]
        public string? Publisher { get; set; }

        public DateTime? PublishedDate { get; set; }

        [MaxLength(50)]
        public string? Edition { get; set; }

        [MaxLength(50)]
        public string? Language { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

        public int? PageCount { get; set; }

        public FormatType? Format { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        [MaxLength(10)]
        public string? Currency { get; set; }

        public int? StockQuantity { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? CoverImageUrl { get; set; }

        public double? Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
