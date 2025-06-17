using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Purchase
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }  // stored in pcs
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
