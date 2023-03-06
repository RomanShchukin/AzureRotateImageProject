using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace RotateBlobImageWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public ImageController(ILogger<ImageController> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        [HttpGet(Name = "Rotate")]
        public async Task<string> Rotate(string path)
        {
            try
            {
                var rotator = new AzureBlobImageRotator(_blobServiceClient);

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