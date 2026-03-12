using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetSavingBackend.Interfaces;
using SixLabors.ImageSharp;

namespace PetSavingBackend.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IImageService _ImageService;

        public ImageController(IWebHostEnvironment env, IImageService ImageService)
        {
            _env = env;
            _ImageService = ImageService;
        }

        [HttpPost("{id:Guid}")]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromRoute] Guid id)
        {
            string? alreadyExist = _ImageService.GetImageExtension(id);

            if (alreadyExist != null)
            {
                return BadRequest("Image already exists, use PUT to overwrite.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (file.Length > 5242880)
            {
                return BadRequest("File size cannot exceed 5MB.");
            }

            bool validImage = _ImageService.IsImageBySignature(file.OpenReadStream());

            if (!validImage)
            {
                return BadRequest("File must be a png or jpg.");
            }

            bool writeSourceFileSuccess = await _ImageService.WriteIFormFileAsync(file, id);

            Image smImage = _ImageService.ToSmallFromSourceAsync(file);

            bool writeSuccess = await _ImageService.WriteImageAsync(smImage, id);

            if (writeSuccess && writeSourceFileSuccess)
            {
                return Ok(new { FilePath = "/Images/source/" + id.ToString() + ".jpg" });
            }
            else
            {
                return StatusCode(500, "A critical error occured during image saving.");
            }
        }

        [HttpGet("sm/{id:Guid}")]
        public IActionResult GetSmImage(Guid id)
        {
            var filePath = Path.Combine(_env.WebRootPath, "Images/sm", id.ToString() + ".jpg");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "image/jpeg"); // Change MIME type based on your file type
        }

        [HttpGet("source/{id:Guid}")]
        public IActionResult GetSourceImage(Guid id)
        {
            string? contentType = _ImageService.GetImageExtension(id);

            if (contentType == null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(_env.WebRootPath, "Images/source", id.ToString() + contentType);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            if (contentType == ".jpg")
            {
                contentType = ".jpeg";
            }

            return File(fileBytes, "image/" + contentType.Substring(1));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateImage(IFormFile file, [FromRoute] Guid id)
        {
            string? alreadyExist = _ImageService.GetImageExtension(id);

            if (alreadyExist == null)
            {
                return BadRequest("Image does not exists, use POST to create a new image.");
            }
            
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (file.Length > 5242880)
            {
                return BadRequest("File size cannot exceed 5MB.");
            }

            bool validImage = _ImageService.IsImageBySignature(file.OpenReadStream());

            if (!validImage)
            {
                return BadRequest("Not a png or jpg.");
            }

            bool writeSourceFileSuccess = await _ImageService.WriteIFormFileAsync(file, id);

            Image smImage = _ImageService.ToSmallFromSourceAsync(file);

            bool writeSuccess = await _ImageService.WriteImageAsync(smImage, id);

            if (writeSuccess && writeSourceFileSuccess)
            {
                return Ok(new { FilePath = "/Images/source/" + id.ToString() + ".jpg" });
            }
            else
            {
                return StatusCode(500, "A critical error occured during image updating.");
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteImage([FromRoute] Guid id)
        {
            bool deleteSuccess = _ImageService.DeleteImage(id);

            if (!deleteSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}