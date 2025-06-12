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
        public decimal UnitPrice { get; set; }

        public decimal TotalAmount => Quantity * UnitPrice;

        [Required]
        public string Barangay { get; set; } = string.Empty;

        [Required]
        public string Municipality { get; set; } = string.Empty;

        // Navigation properties
        [Required]
        public Salesman Salesman { get; set; } = null!;

        [Required]
        public Item Item { get; set; } = null!;
    }
}
