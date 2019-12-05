namespace Librairie.ViewModels
{
    using System.Collections.Generic;

    public class UserVM
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public List<BookVM> Books { get; set; }
    }
}