using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCVSharp_dotnet6_docker.Services;

namespace OpenCVSharp_dotnet6_docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile photo)
        {
            try
            {
                string uploadFolder = Path.Combine(AppContext.BaseDirectory, "Images");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                if (photo == null || photo.Length == 0)
                {
                    return Content("File not selected");
                }
                var path = Path.Combine(uploadFolder, photo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                    stream.Close();
                }
                bool isBlurry = ImageProcessingService.IsBlurry(path);
            }
            catch (Exception ex)
            {

            }
           
            return Ok();
        }


    }
}
