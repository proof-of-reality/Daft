using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer;

public class SqlServerContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public DbSet<Property> Properties { get; set; }

    public DbSet<Facility> Facilities { get; set; }

    public DbSet<Photo> Photos { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Daft;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        base.OnConfiguring(optionsBuilder);
    }
}
