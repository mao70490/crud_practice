using System.ComponentModel.DataAnnotations;
using CrudPractice1.Enums;

namespace CrudPractice1.Models
{
    public class Movie2
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public GenreType? Genre { get; set; }
        public decimal Price { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Director { get; set; }
        public string? Cast { get; set; }
        public int DurationMinutes { get; set; }
        public ICollection<MovieLanguage> MovieLanguages { get; set; } = new List<MovieLanguage>();
        public string? Country { get; set; }
        public string? Rating { get; set; }
        public int ImdbScore { get; set; }
        public long BoxOffice { get; set; }
        public string? Synopsis { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string? ProductionCompany { get; set; }
        public long Budget { get; set; }
        public string? Awards { get; set; }
    }
}
