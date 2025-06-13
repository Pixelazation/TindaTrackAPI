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
        public int BarangayId { get; set; }

        // Navigation properties
        public required Salesman Salesman { get; set; }

        public required Item Item { get; set; }
        public required Barangay Barangay { get; set; }
    }
}
