using Microsoft.AspNetCore.Mvc;

namespace RealEstate.PL.Services.UploadFile
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<FileStreamResult> DownloadFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
    }

}
