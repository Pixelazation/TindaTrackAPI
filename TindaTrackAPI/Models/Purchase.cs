using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }  // stored in pcs

        [Required]
        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be positive.")]
        public decimal UnitPrice { get; set; }

        public decimal TotalAmount => Quantity * UnitPrice;

        // Navigation properties
        public Item Item { get; set; } = null!;
    }
}
