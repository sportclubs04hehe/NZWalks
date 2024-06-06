using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Implements
{
    public class SQLImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalksDbContext _context;

        public SQLImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor contextAccessor,
            NZWalksDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Images", $"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create); // tạo nếu chưa có nếu có ghi đè
            await image.File.CopyToAsync(stream);

            //https://example:1234/hehe.com
            var urlPath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{_httpContextAccessor.HttpContext.Request.Host}" +
                $"{_httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlPath;

            // Add Image to the Images Tables
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
