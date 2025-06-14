using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public int SalesmanId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }  // stored in pcs

        [Required]
        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be positive.")]
        public decimal UnitPrice { get; set; }

        public decimal TotalAmount => Quantity * UnitPrice;

        [Required]
        public int BarangayId { get; set; }

        // Navigation properties
        public Salesman Salesman { get; set; } = null!;
        public Item Item { get; set; } = null!;
        public Barangay Barangay { get; set; } = null!;
    }
}
