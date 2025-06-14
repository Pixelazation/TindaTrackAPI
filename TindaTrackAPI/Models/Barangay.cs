using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TindaTrackAPI.Models
{
    public class Barangay
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "json")]
        public required string GeoJsonBoundary { get; set; }

        [Required]
        public int MunicipalityId { get; set; }

        public required Municipality Municipality { get; set; }

        public ICollection<Sale> Sales { get; set; } = null!;
    }

}
