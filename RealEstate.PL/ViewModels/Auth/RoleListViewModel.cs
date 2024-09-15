namespace RealEstate.PL.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; } 
        public string RoleName { get; set; }
    }

    public class RoleListViewModel
    {
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}
