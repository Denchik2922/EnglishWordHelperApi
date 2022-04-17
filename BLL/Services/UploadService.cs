using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UploadService : IUploadService
    {
        private string _baseUrl;

        public UploadService(IConfiguration config)
        {
            _baseUrl = config["StaticFilesOptions:BaseUrl"];
        }

        public async Task<string> Upload(IFormFile file, string filePath, string fileName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(filePath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return _baseUrl + dbPath;
        }
    }
}
