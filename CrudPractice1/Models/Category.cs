namespace CrudPractice1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
