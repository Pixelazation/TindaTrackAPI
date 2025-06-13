using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TindaTrackAPI.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public required string ItemCode { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public required ICollection<Sale> Sales { get; set; }
    }

}
