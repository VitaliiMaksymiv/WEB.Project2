namespace Librairie.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public virtual List<AuthorBook> AuthorBooks { get; set; }

        public virtual List<UserBook> UserBooks { get; set; }
    }
}