using Microsoft.EntityFrameworkCore;
using Odem.WebAPI.Models;
namespace Odem.WebAPI;

public class DataContext : DbContext
{
    public DbSet<Client>? Clients { get; set; }
    public DbSet<Admin>? Admins { get; set; }
    public DbSet<BankTransfer>? BankTransfers { get; set; }
    public DbSet<LocalTransfer>? LocalTransfers { get; set; }
    public DbSet<OdemTransfer>? OdemTransfers { get; set; }
    public DbSet<Ticket>? Tickets { get; set; }
    public DbSet<Wallet>? Wallets { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=85.215.99.211;User ID=sa;Password=153759759mM;Database=Odemdb;TrustServerCertificate=True;Trusted_Connection=True;Integrated Security=false;");
    }
}