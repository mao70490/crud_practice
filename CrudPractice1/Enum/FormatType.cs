using System.ComponentModel.DataAnnotations;

namespace CrudPractice1.Enums
{
    public enum FormatType
    {
        [Display(Name = "平裝書")]
        Paperback,

        [Display(Name = "精裝書")]
        Hardcover,

        [Display(Name = "電子書")]
        Ebook
    }
}
