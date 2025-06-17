using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Account
{
    public class CreateAccountDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public int BarangayId { get; set; }
    }
}
