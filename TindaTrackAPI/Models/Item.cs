using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.Models
{
    public class Item
    {
        [Key]
        public required string ItemCode { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required ICollection<Sale> Sales { get; set; }
    }

}
