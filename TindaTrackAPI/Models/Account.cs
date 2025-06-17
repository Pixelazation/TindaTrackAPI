using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required int BarangayId { get; set; }

        // Navigation
        public Barangay Barangay { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = default!;
    }
}
