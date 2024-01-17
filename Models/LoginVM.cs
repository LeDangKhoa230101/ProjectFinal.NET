using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.NET.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Chưa đúng định dạng")]
        [EmailAddress]
        [Display(Name = "nhập gmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
