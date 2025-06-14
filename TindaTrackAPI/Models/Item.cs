using Microsoft.EntityFrameworkCore;
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

        [Required]
        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be positive.")]
        public required decimal UnitPrice { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }

}
