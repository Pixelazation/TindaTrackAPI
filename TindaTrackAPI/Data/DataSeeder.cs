using TindaTrackAPI.Data.Seeders;

namespace TindaTrackAPI.Data;

public static class DataSeeder
{
    public static void Seed(TindaTrackContext context)
    {
        LocationSeeder.Seed(context);
    }
}
