using System;
using System.Collections.Generic;

namespace RealEstate.PL.ViewModels
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public decimal Price { get; set; }
        public int SquareFootage { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string PropertyType { get; set; }
        public DateTime ListedDate { get; set; }
        public string Description { get; set; }
        public List<string> Features { get; set; } = new List<string>();
        public List<string> PhotoUrls { get; set; } = new List<string>();
        public string Status { get; set; }
        public bool HasGarage { get; set; }
        public bool HasPool { get; set; }
        public bool IsFurnished { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhone { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string IpAddress { get; set; }
    }
}
