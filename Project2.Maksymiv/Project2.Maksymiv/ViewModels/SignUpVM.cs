namespace Librairie.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SignUpVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not equal")]
        public string ConfirmPassword { get; set; }
    }
}