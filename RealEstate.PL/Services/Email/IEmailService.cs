namespace RealEstate.PL.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null);
    }

}
