using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RealEstate.PL.ViewModels.Auth;
public class SettingsController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public SettingsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        var model = new SettingsViewModel
        {
            FullName = user.UserName,
            Email = user.Email,
            GeneralSettings = new Dictionary<string, string>
            {
                { "Theme", "Light" }, // Example
                { "Language", "English" }
            },
            NotificationPreferences = new Dictionary<string, bool>
            {
                { "EmailNotifications", true },
                { "SmsNotifications", false }
            },
            ChangePasswordModel = new ChangePasswordViewModel()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSettings(SettingsViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            user.UserName = model.FullName;
            user.Email = model.Email;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            if (!string.IsNullOrEmpty(model.ChangePasswordModel.CurrentPassword) &&
                !string.IsNullOrEmpty(model.ChangePasswordModel.NewPassword) &&
                model.ChangePasswordModel.NewPassword == model.ChangePasswordModel.ConfirmNewPassword)
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user,
                    model.ChangePasswordModel.CurrentPassword, model.ChangePasswordModel.NewPassword);

                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Success"] = "Settings updated successfully!";
                return RedirectToAction("Index");
            }
        }

        return View("Index", model);
    }
}
