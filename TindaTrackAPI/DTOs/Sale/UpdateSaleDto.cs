using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Sale
{
    public class UpdateSaleDto
    {
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }

        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be positive.")]
        public decimal? UnitPrice { get; set; }
        public int? SalesmanId { get; set; }
        public int? ItemId { get; set; }
        public int? BarangayId { get; set; }
    }
}
