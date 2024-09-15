using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RealEstate.PL.Services.UploadFile
{
    public class VideoService : IVideoService
    {
        private readonly string _videoStoragePath;

        public VideoService(IWebHostEnvironment webHostEnvironment)
        {
            _videoStoragePath = Path.Combine(webHostEnvironment.WebRootPath, "videos");
            if (!Directory.Exists(_videoStoragePath))
            {
                Directory.CreateDirectory(_videoStoragePath);
            }
        }

        public async Task<string> UploadVideoAsync(IFormFile video)
        {
            if (video == null || video.Length == 0)
                return null;

            var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(video.FileName);
            var filePath = Path.Combine(_videoStoragePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await video.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<FileStreamResult> DownloadVideoAsync(string videoFileName)
        {
            var filePath = Path.Combine(_videoStoragePath, videoFileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var contentType = GetContentType(videoFileName);

            return new FileStreamResult(memory, contentType)
            {
                FileDownloadName = videoFileName
            };
        }

        public async Task<bool> DeleteVideoAsync(string videoFileName)
        {
            var filePath = Path.Combine(_videoStoragePath, videoFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }

}
