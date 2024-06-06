using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Implements;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        //UPLOAD IMAGE
        // POST: https://localhost:7010/api/images/upload
        // [FromForm] => định dạng multipart/form-data hoặc application/x-www-form-urlencoded 
        [HttpPost("upload")]
        [ValidateModel]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto uploadDto)
        {
            ValidateFileUpload(uploadDto);

            //Conert to DTO
            var imageDomainModel = new Image
            {
                File = uploadDto.File,
                FileExtension = Path.GetExtension(uploadDto.File.FileName),
                FileSizeInBytes = uploadDto.File.Length,
                FileName = uploadDto.FileName,
                FileDescription = uploadDto.FileDescription,
            };

            await _imageRepository.Upload(imageDomainModel);

            return Ok(imageDomainModel);
        }

        private void ValidateFileUpload(ImageUploadDto imageUploadDto)
        {
            var allowExtensions = new string[] { ".jpg", ".png", ".jpeg" };

            //Path.GetExtension => lấy đuôi file 
            if (!allowExtensions.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            // 10485760 => 10mb
            if (imageUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file");
            }
        }
    }
}
