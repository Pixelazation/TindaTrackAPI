using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TindaTrackAPI.Models
{
    public class Municipality
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "json")]
        public required string GeoJsonBoundary { get; set; }

        public required ICollection<Barangay> Barangays { get; set; }
    }
}
