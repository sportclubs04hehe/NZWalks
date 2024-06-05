using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class CreateRegionDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Mã Code không được quá 3 ký tự")]
        [MinLength(3, ErrorMessage = "Mã Code phải là 3 ký tự")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tên không được quá 100 ký tự")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
