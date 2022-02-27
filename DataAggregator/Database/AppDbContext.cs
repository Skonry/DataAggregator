using Microsoft.EntityFrameworkCore;
using DataAggregator.Entities;

namespace DataAggregator.Database;

public class AppDbContext : DbContext
{
    public DbSet<SourceEntity> Sources { get; set; }
    public DbSet<StreamEntity> Streams { get; set; }
    public DbSet<FilterEntity> Filters { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
   
}
