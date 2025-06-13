using System.ComponentModel.DataAnnotations;

namespace TindaTrackAPI.DTOs.Barangay
{
    public class BarangayDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MunicipalityName { get; set; } = string.Empty;
    }
}
