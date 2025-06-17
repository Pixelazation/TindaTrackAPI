namespace TindaTrackAPI.DTOs.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string BarangayName { get; set; } = default!;
        public string MunicipalityName { get; set; } = default!;
    }
}
