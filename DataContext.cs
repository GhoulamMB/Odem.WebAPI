using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
namespace Odem.WebAPI;

public class DataContext : DbContext
{
    public DbSet<Client>? Clients { get; set; }
    public DbSet<Admin>? Admins { get; set; }
    public DbSet<OdemTransfer>? OdemTransfers { get; set; }
    public DbSet<Ticket>? Tickets { get; set; }
    public DbSet<Wallet>? Wallets { get; set; }
    public DbSet<TransferRequest>? TransferRequests { get; set; }
    public DbSet<OneSignalIds>? OneSignalIds { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Wallet>()
            .HasMany<OdemTransfer>(w=>w.Transactions)
            .WithOne(f=>f.From);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer("Server=0.0.0.0;User ID=sa;Password=pwd;Database=Odemdbdev;TrustServerCertificate=True;Trusted_Connection=True;Integrated Security=false;"
            ,so=>so.EnableRetryOnFailure()
            );
        /*.UseNpgsql("Host=0.0.0.0;Database=odemdb;Username=sa;Password=pwd",
        opt => opt.EnableRetryOnFailure()
            );
        */
    }
}