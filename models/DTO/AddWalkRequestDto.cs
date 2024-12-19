using NzWalks.API.models.domains;

using System.ComponentModel.DataAnnotations;

namespace NzWalks.API.models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }




        //navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}