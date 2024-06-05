using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Tên không được quá 200 ký tự")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Mô tả không được quá 1000 ký tự")]
        public string Description { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Chuyến đi không được quá 1000km")]
        public double LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
