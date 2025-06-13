using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Salesman
{
    public class CreateSalesmanDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
