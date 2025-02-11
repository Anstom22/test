using System.ComponentModel.DataAnnotations;

namespace Sample1.Models.DTO
{
    public class AddRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage ="Min length of 3 chars required")]
        [MaxLength(3,ErrorMessage="Max length is 3 chars")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="Max length is 100 chars")]
        public string Name { get; set; }
    }
}

