using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.NET.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Chưa đúng định dạng")]
        [EmailAddress]
        [Display(Name ="nhập gmail")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "ko same r")]
        public string ConfirmPassword { get; set; }
    }
}
