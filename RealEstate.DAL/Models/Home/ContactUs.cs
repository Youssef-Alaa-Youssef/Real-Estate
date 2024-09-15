using System;

namespace RealEstate.DAL.Models.Home
{
    public class ContactUs
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string IpAddress { get; set; }
    }
}
