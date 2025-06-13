using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Item
{
    public class UpdateItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be positive.")]
        public decimal? UnitPrice { get; set; }
    }
}
