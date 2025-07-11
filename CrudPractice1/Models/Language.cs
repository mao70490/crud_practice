using System.ComponentModel.DataAnnotations;

namespace CrudPractice1.Models
{
    public class Language
    {
        [Key]
        public string Code { get; set; } = null!;     // zh, en, jp（當主鍵）
        public string Name { get; set; } = null!;     // 中文、英文、日文

        public ICollection<MovieLanguage> MovieLanguages { get; set; } = new List<MovieLanguage>();
    }
}
