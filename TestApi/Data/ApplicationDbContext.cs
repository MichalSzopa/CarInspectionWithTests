using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseSqlite("Data Source=sqliteDatabase.db");
    }

    public DbSet<Car> Cars { get; set; }

    public DbSet<Inspection> Inspections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("Cars");
        });

        modelBuilder.Entity<Inspection>(entity =>
        {
            entity.ToTable("Inspections");

            entity.HasOne(i => i.Car)
                  .WithMany(c => c.Inspections)
                  .HasForeignKey(i => i.CarId)
                  .IsRequired(true);
        });

        Database.Migrate();
    }
}