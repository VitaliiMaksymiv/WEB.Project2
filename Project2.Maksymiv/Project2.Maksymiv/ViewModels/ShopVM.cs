namespace Librairie.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents Shop View Model.
    /// </summary>
    public class ShopVM
    {
        /// <summary>
        /// Gets or sets <see cref="List{T}"/> of <see cref="BookVM"/>.
        /// </summary>
        public List<BookVM> ShopItems { get; set; }

        /// <summary>
        /// Gets or sets quantity of shop items on page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets number of page.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets page quantity.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets <see cref="OrderByType"/>.
        /// </summary>
        public OrderByType OrderBy { get; set; }

        /// <summary>
        /// Gets or sets value to find.
        /// </summary>
        public string ToFind { get; set; }
    }
}