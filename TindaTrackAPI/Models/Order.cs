using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public int SalesmanId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public decimal TotalSales => Purchases.Sum(p => p.TotalAmount);

        // Navigation
        public Account Account { get; set; } = null!;
        public Salesman Salesman { get; set; } = null!;
        public ICollection<Purchase> Purchases { get; set; } = null!;
    }
}
