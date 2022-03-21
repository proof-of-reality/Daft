using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer;

public class SqlServerContext : DbContext
{
    public SqlServerContext(DbContextOptions<SqlServerContext> context) : base(context)
    {
    }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Property> Properties { get; set; }

    public DbSet<Facility> Facilities { get; set; }

    public DbSet<Photo> Photos { get; set; }

    public DbSet<User> Users { get; set; }
}
