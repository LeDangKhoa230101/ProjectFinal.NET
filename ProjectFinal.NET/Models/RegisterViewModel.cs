using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.NET.Models
{
    public class RegisterViewModel

    {
        [Display(Name ="Nhập gmail đăng ký")]
        [Required (ErrorMessage ="*")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Nhập pass đăng ký")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
