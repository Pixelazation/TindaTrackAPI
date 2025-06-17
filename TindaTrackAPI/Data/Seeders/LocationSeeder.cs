using CsvHelper;
using System.Globalization;
using System.IO;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.Data.Seeders;

public static class LocationSeeder
{
    public static void Seed(TindaTrackContext context)
    {
        if (context.Barangays.Any() || context.Municipalities.Any()) return;

        using var reader = new StreamReader("Data/CSVs/cebu_locations.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var municipalities = new Dictionary<string, Municipality>();
        var records = csv.GetRecords<LocationCsv>();

        foreach (var record in records)
        {
            var barangayName = record.Barangay.Trim();
            var municipalityName = record.Municipality.Trim();

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

    private class LocationCsv
    {
        public string Barangay { get; set; } = string.Empty;
        public string Municipality { get; set; } = string.Empty;
    }
}
