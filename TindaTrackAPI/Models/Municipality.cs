﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TindaTrackAPI.Models
{
    public class Municipality
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<Barangay> Barangays { get; set; } = null!;
    }
}
