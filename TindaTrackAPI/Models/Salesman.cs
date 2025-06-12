using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.Models
{
    public class Salesman
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        // Navigation
        [Required]
        public required ICollection<Sale> Sales { get; set; }
    }

}
