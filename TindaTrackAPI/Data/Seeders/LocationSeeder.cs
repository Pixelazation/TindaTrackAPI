using TindaTrackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TindaTrackAPI.Data.Seeders
{
    public static class LocationSeeder
    {
        public static void Seed(TindaTrackContext context)
        {
            if (context.Barangays.Any() || context.Municipalities.Any()) return;

            var lines = File.ReadAllLines("Data/CSVs/cebu_locations.csv").Skip(1); // skip header
            var municipalities = new Dictionary<string, Municipality>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var barangayName = parts[0].Trim();
                var municipalityName = parts[1].Trim();

                if (!municipalities.TryGetValue(municipalityName, out var municipality))
                {
                    municipality = new Municipality { Name = municipalityName };
                    context.Municipalities.Add(municipality);
                    municipalities[municipalityName] = municipality;
                }

                var barangay = new Barangay
                {
                    Name = barangayName,
                    Municipality = municipality
                };

                context.Barangays.Add(barangay);
            }

            context.SaveChanges();
        }
    }
}
