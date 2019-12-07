namespace Librairie.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser<int>
    {
        public virtual List<UserBook> UserBooks { get; set; }
    }
}