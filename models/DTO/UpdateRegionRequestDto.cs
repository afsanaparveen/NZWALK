using System.ComponentModel.DataAnnotations;

namespace NzWalks.API.models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code as to minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code as to maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MinLength(100)]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
