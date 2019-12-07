namespace Librairie.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public enum OrderByType
    {
        Default = 0,

        [Display(Name = "Price ↑")]
        PriceUp,

        [Display(Name = "Price ↓")]
        PriceDown,

        [Display(Name = "Title ↑")]
        TitleUp,

        [Display(Name = "Title ↓")]
        TitleDown,

        [Display(Name = "Author ↑")]
        AuthorUp,

        [Display(Name = "Author ↓")]
        AuthorDown,
    }
}
