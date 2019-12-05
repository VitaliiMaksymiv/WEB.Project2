namespace Librairie.ViewModels
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class AuthorVM
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public List<BookVM> Books { get; set; }
    }
}
