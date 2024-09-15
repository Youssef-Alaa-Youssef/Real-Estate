namespace RealEstate.DAL.Models
{
    public class SettingGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LinkNameEn { get; set; }
        public string LinkNameAr { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Visable { get; set; } 
        public string ranking { get; set; }
        public string place { get; set; }
        public string Permission { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
