namespace CrudPractice1.Models
{
    public class MovieLanguage
    {
        public int Movie2Id { get; set; }
        public Movie2 Movie2 { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;
        public Language Language { get; set; } = null!;
    }
}
