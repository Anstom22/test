using System.ComponentModel.DataAnnotations;

namespace Sample1.Models.DTO
{
    public class AddWalkDTO
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public int DifficultyId { get; set; }
        [Required]
        public int RegionId { get; set; }
    }
}
