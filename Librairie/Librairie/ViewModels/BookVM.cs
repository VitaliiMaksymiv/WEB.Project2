namespace Librairie.ViewModels
{
    using System.Collections.Generic;

    public class BookVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public List<AuthorVM> Authors { get; set; }

        public List<UserVM> Users { get; set; }
    }
}
