using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Sale
{
    public class CreateSaleDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be positive.")]
        public required decimal UnitPrice { get; set; }

        [Required]
        public int SalesmanId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int BarangayId { get; set; }
    }
}
