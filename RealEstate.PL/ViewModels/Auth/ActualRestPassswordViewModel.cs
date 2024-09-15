using System.ComponentModel.DataAnnotations;

namespace RealEstate.PL.ViewModels.Auth
{
    public class ActualRestPassswordViewModel
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}