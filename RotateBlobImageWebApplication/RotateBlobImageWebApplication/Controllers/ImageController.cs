using Azure.Core.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace RotateBlobImageWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Rotate")]
        public async Task<string> Rotate(string path)
        {
            try
            {
                var rotator = new AzureBlobImageRotator();

                var rotatedImagePath = await rotator.RotateImage(path);

                return rotatedImagePath;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Rotate exception");

                return ex.Message;
            }
        }
    }
}