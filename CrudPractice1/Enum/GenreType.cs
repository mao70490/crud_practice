using System.ComponentModel.DataAnnotations;

namespace CrudPractice1.Enums
{
    public enum GenreType
    {
        [Display(Name = "動作")]
        Action = 1,

        [Display(Name = "愛情")]
        Romance = 2,

        [Display(Name = "科幻")]
        SciFi = 3,

        [Display(Name = "恐怖")]
        Horror = 4
    }
}
