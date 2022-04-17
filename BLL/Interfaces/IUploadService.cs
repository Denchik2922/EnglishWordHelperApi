using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUploadService
    {
        Task<string> Upload(IFormFile file, string filePath, string fileName);
    }
}