namespace TindaTrackAPI.DTOs.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public string BarangayName { get; set; } = string.Empty;
        public string MunicipalityName { get; set; } = string.Empty;
        public string SalesmanName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
    }
}
