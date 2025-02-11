using Sample1.Models.Domain;

namespace Sample1.Models.DTO
{
    public class WalkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public int DifficultyId { get; set; }
        public int RegionId { get; set; }

        public DifficultyDTO Difficulty{ get; set; }
        public RegionDTO Region { get; set; }
    }
}
