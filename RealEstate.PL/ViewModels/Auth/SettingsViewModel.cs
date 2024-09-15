namespace RealEstate.PL.ViewModels.Auth
{
    public class SettingsViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public Dictionary<string, string> GeneralSettings { get; set; }

        public Dictionary<string, bool> NotificationPreferences { get; set; }

        public ChangePasswordViewModel ChangePasswordModel { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
