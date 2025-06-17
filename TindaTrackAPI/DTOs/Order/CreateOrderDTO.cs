using TindaTrackAPI.DTOs.Purchase;

namespace TindaTrackAPI.DTOs.Order
{
    public class CreateOrderDTO
    {
        public int AccountId { get; set; }
        public int SalesmanId { get; set; }
        public DateTime Date { get; set; }
        public List<CreatePurchaseDto> Purchases { get; set; } = default!;
    }
}
