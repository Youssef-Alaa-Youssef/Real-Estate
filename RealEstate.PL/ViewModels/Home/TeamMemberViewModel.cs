using System.ComponentModel.DataAnnotations;

namespace RealEstate.PL.ViewModels
{
    public class TeamMemberViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the team member's name.")]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters.")]
        [Display(Name = "Name", Prompt = "Enter the team member's name")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "The role cannot exceed 50 characters.")]
        [Display(Name = "Role", Prompt = "Enter the role of the team member")]
        public string Role { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the profile image.")]
        [StringLength(255, ErrorMessage = "The profile image URL cannot exceed 255 characters.")]
        [Display(Name = "Profile Image URL", Prompt = "Enter the URL for the profile image")]
        public string ProfileImageUrl { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the icon.")]
        [StringLength(255, ErrorMessage = "The icon URL cannot exceed 255 characters.")]
        [Display(Name = "Icon URL", Prompt = "Enter the URL for the icon")]
        public string IconUrl { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(150, ErrorMessage = "The email address cannot exceed 150 characters.")]
        [Display(Name = "Email", Prompt = "Enter the team member's email address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(20, ErrorMessage = "The phone number cannot exceed 20 characters.")]
        [Display(Name = "Phone", Prompt = "Enter the team member's phone number")]
        public string Phone { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the Facebook link.")]
        [StringLength(255, ErrorMessage = "The Facebook link cannot exceed 255 characters.")]
        [Display(Name = "Facebook Link", Prompt = "Enter the URL for the Facebook profile")]
        public string FacebookLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the Twitter link.")]
        [StringLength(255, ErrorMessage = "The Twitter link cannot exceed 255 characters.")]
        [Display(Name = "Twitter Link", Prompt = "Enter the URL for the Twitter profile")]
        public string TwitterLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the LinkedIn link.")]
        [StringLength(255, ErrorMessage = "The LinkedIn link cannot exceed 255 characters.")]
        [Display(Name = "LinkedIn Link", Prompt = "Enter the URL for the LinkedIn profile")]
        public string LinkedInLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the Instagram link.")]
        [StringLength(255, ErrorMessage = "The Instagram link cannot exceed 255 characters.")]
        [Display(Name = "Instagram Link", Prompt = "Enter the URL for the Instagram profile")]
        public string InstagramLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the YouTube link.")]
        [StringLength(255, ErrorMessage = "The YouTube link cannot exceed 255 characters.")]
        [Display(Name = "YouTube Link", Prompt = "Enter the URL for the YouTube channel")]
        public string YouTubeLink { get; set; }
    }
}
