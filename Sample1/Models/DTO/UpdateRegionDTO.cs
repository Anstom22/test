using System.ComponentModel.DataAnnotations;

namespace Sample1.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Max length is 3")]
        [MinLength(3,ErrorMessage ="Min length is 3")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Max length is 100")]
        public string Name { get; set; }
    }
}
