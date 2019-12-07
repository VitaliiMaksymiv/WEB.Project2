namespace Librairie.Models
{
    public class UserBook
    {
        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}