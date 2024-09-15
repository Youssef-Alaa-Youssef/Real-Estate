using System.ComponentModel.DataAnnotations;

namespace RealEstate.PL.ViewModels.Home
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [Display(Prompt = "Enter Name ...")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(150, ErrorMessage = "Email cannot be longer than 150 characters.")]
        [Display(Prompt = "Enter Email ...")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(1000, ErrorMessage = "Message cannot be longer than 1000 characters.")]
        [Display(Prompt = "Enter Your Messages ...")]

        public string Message { get; set; }
    }
}
