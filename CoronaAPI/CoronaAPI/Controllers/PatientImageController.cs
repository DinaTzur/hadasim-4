using CoronaAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientImageController : ControllerBase
    {
        private readonly string _imagesDirectory; // Directory to save images

        public PatientImageController()
        {
            // Specify the directory where images will be saved
            _imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");
            if (!Directory.Exists(_imagesDirectory))
            {
                Directory.CreateDirectory(_imagesDirectory);
            }
        }
        // POST api/<PatientImageController>
        [HttpPost]
        public async Task<IActionResult> UploadImageAsync([FromForm] ImageUploadModel model)
        {
            if (model == null || model.ImageFile == null || model.Id <= 0)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                // Generate a unique filename for the image
                var uniqueFileName = $"{model.Id}{model.ImageFile.FileName.Substring(model.ImageFile.FileName.LastIndexOf("."))}";

                // Combine the directory path and filename to get the full path
                var filePath = Path.Combine(_imagesDirectory, uniqueFileName);

                // Save the image file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                // You can save the filePath to your database or perform other operations here

                return Ok(new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<PatientImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] List<IFormFile> files)
        {

        }
    }
}
