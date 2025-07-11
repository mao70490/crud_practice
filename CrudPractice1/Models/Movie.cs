using System.ComponentModel.DataAnnotations;

namespace CrudPractice1.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Director { get; set; }
        public string? Cast { get; set; }
        public int DurationMinutes { get; set; }
        public string? Language { get; set; }
        public string? Country { get; set; }
        public string? Rating { get; set; }
        public double ImdbScore { get; set; }
        public long BoxOffice { get; set; }
        public string? Synopsis { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string? ProductionCompany { get; set; }
        public long Budget { get; set; }
        public string? Awards { get; set; }
    }
}
