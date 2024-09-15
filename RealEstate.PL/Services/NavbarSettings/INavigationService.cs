using RealEstate.PL.ViewModels.Admin;

namespace RealEstate.PL.Services.NavbarSettings
{
    public interface INavigationService
    {
        Task<List<NavbarLinkViewModel>> GetNavbarLinksAsync();

    }
}
