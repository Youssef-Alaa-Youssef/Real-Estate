using System;
using System.Collections.Generic;
using System.Linq;
namespace RealEstate.DAL.Models.Home
{
    public class TeamMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string ProfileImageUrl { get; set; }

        public string IconUrl { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string IpAddress { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsHidden { get; set; }


        public string FacebookLink { get; set; }

        public string TwitterLink { get; set; }

        public string LinkedInLink { get; set; }

        public string InstagramLink { get; set; }

        public string YouTubeLink { get; set; }
    }
}
