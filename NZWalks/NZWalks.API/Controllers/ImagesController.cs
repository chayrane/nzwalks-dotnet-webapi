using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/images")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageRepository _imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    // Uploading Image.
    // POST: /api/images/upload
    [HttpPost]
    [Route("upload")]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
    {
        ValidateFileUpload(request);

        if (ModelState.IsValid)
        {
            // Map DTO to Domain Model.
            var imageDomainModel = new Image
            {
                File = request.File,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };

            // User repository to upload image.
            await _imageRepository.Upload(imageDomainModel);

            return Ok(imageDomainModel);
        }

        return BadRequest(ModelState);
    }

    private void ValidateFileUpload(ImageUploadRequestDto request)
    {
        var allowedExtensions = new string[]
        {
            ".jpg", ".jpeg", ".png"
        };

        // Check if the extension is present in allowedExtensions.
        if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
        {
            ModelState.AddModelError("file", "Unsupported file extension...!");
        }

        // Check if the size of the file is more than 10MB.
        if (request.File.Length > 10485760)
        {
            ModelState.AddModelError(
                "file", "File size is more than 10MB, please upload a smaller size file!");
        }
    }
}
