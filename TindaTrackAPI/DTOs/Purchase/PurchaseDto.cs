using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TindaTrackAPI.DTOs.Item;

namespace TindaTrackAPI.DTOs.Purchase
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ItemDto Item { get; set; } = default!;
        public int Quantity { get; set; }  // stored in pcs
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
