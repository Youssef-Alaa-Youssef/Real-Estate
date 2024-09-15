using RealEstate.DAL.Models.Home;

namespace RealEstate.PL.ViewModels
{
    public class ContactUsDashboardViewModel
    {
        public List<ContactUs> Contacts { get; set; }
        public List<ContactStats> ContactStats { get; set; }
    }

    public class ContactStats
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }
}
