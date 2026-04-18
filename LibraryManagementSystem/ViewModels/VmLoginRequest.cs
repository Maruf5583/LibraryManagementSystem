using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModels
{
    public class VmLoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
