using Microsoft.EntityFrameworkCore;
using TindaTrackAPI.Models;

public class TindaTrackContext : DbContext
{
    public TindaTrackContext(DbContextOptions<TindaTrackContext> options)
        : base(options) { }

    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Salesman> Salesmen { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Barangay> Barangays { get; set; }
    public DbSet<Municipality> Municipalities { get; set; }
}
