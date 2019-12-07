namespace Librairie.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShopVM
    {
        public List<BookVM> ShopItems { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }

        public OrderByType OrderBy { get; set; }

        public string ToFind { get; set; }
    }
}