using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TindaTrackAPI.Models
{
    public class Salesman
    {
        public int Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        // Navigation
        public required ICollection<Sale> Sales { get; set; }
    }

}
