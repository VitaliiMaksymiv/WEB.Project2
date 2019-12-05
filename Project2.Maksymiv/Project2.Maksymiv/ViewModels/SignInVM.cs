namespace Librairie.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SignInVM
    {
        [Required]
        [UIHint("email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}