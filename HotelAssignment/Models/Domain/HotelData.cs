using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace HotelAssignment.Models.Domain
{
    public class HotelData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }

        public string Path { get; set; }

        [NotMapped]
        public IFormFile ImagePath { get; set; }
    }
}
