using System.ComponentModel.DataAnnotations;

namespace RealEstate.PL.ViewModels.Property
{
    public class PropertyViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City can't be longer than 100 characters.")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required.")]
        [StringLength(100, ErrorMessage = "State can't be longer than 100 characters.")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is required.")]
        [StringLength(10, ErrorMessage = "Zip Code can't be longer than 10 characters.")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code format.")]
        public string ZipCode { get; set; }

        [Display(Name = "Price ($)")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Display(Name = "Square Footage")]
        [Required(ErrorMessage = "Square Footage is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Square Footage must be a positive number.")]
        public int SquareFootage { get; set; }

        [Display(Name = "Bedrooms")]
        [Required(ErrorMessage = "Number of Bedrooms is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of Bedrooms must be a non-negative number.")]
        public int Bedrooms { get; set; }

        [Display(Name = "Bathrooms")]
        [Required(ErrorMessage = "Number of Bathrooms is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of Bathrooms must be a non-negative number.")]
        public int Bathrooms { get; set; }

        [Display(Name = "Property Type")]
        [Required(ErrorMessage = "Property Type is required.")]
        [StringLength(100, ErrorMessage = "Property Type can't be longer than 100 characters.")]
        public string PropertyType { get; set; }

        [Display(Name = "Listed Date")]
        [Required(ErrorMessage = "Listed Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ListedDate { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; }

        [Display(Name = "Features")]
        [StringLength(500, ErrorMessage = "Features list can't be longer than 500 characters.")]
        public List<string> Features { get; set; } = new List<string>();

        [Display(Name = "Photo URLs")]
        [StringLength(500, ErrorMessage = "Photo URLs list can't be longer than 500 characters.")]
        public List<string> PhotoUrls { get; set; } = new List<string>();

        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters.")]
        public string Status { get; set; }

        [Display(Name = "Has Garage")]
        public bool HasGarage { get; set; }

        [Display(Name = "Has Pool")]
        public bool HasPool { get; set; }

        [Display(Name = "Is Furnished")]
        public bool IsFurnished { get; set; }

        [Display(Name = "Agent Name")]
        [StringLength(100, ErrorMessage = "Agent Name can't be longer than 100 characters.")]
        public string AgentName { get; set; }

        [Display(Name = "Agent Email")]
        [Required(ErrorMessage = "Agent Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(150, ErrorMessage = "Agent Email can't be longer than 150 characters.")]
        public string AgentEmail { get; set; }

        [Display(Name = "Agent Phone")]
        [Required(ErrorMessage = "Agent Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(20, ErrorMessage = "Agent Phone can't be longer than 20 characters.")]
        public string AgentPhone { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Created Date")]
        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "IP Address")]
        [StringLength(45, ErrorMessage = "IP Address can't be longer than 45 characters.")]
        public string IpAddress { get; set; }

        // For handling file uploads
        [Display(Name = "Photos")]
        public IFormFile[] Photos { get; set; }
    }
}
