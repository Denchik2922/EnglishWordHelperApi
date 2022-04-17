using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EnglishWordHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private string _imagePath;
        private string _audioPath;

        public UploadController(IUploadService uploadService, IConfiguration config)
        {
            _audioPath = config["StaticFilesOptions:PathForAudio"];
            _imagePath = config["StaticFilesOptions:PathForImage"];
            _uploadService = uploadService;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage()
        {
            string pathOfFile = "";
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            if (file.Length > 0)
            {
                var userName = User.Identity.Name;
                var fileName = userName + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                pathOfFile = await _uploadService.Upload(file, _imagePath, fileName);

                return Ok(pathOfFile);
            }

            return BadRequest("File length must be longer than 0!");
        }

        [HttpPost("audio")]
        public async Task<IActionResult> UploadAudio()
        {
            string pathOfFile = "";
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            if (file.Length > 0)
            {
                var userName = User.Identity.Name;
                var fileName = userName + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                pathOfFile = await _uploadService.Upload(file, _audioPath, fileName);

                return Ok(pathOfFile);
            }

            return BadRequest("File length must be longer than 0!");
        }
    }
}
