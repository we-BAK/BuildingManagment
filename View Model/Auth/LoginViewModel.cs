using System.ComponentModel.DataAnnotations;

namespace BMS.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        [Display(Name = "Username or Email")]
        public string Identity { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string FingerPrint { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}