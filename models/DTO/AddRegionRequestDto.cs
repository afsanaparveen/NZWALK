
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NzWalks.API.models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code as to minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code as to maximum of 3 characters")]

        public required string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }


}
