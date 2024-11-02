using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<District> Districts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<FilterResult> FilterResults { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FilterResult>().OwnsMany(
            d => d.ResultData,
            builder => { builder.ToJson(); });
    }
}
