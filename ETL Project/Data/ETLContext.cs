using ETL_Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL_Project.Data;

public class ETLContext : DbContext
{
    public DbSet<Vendor> Vendors => Set<Vendor>();

    public ETLContext(DbContextOptions<ETLContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendors");

            entity.Property(m => m.Id).HasColumnName("VendorID");
            entity.Property(m => m.PickupTime).HasColumnName("tpep_pickup_datetime");
            entity.Property(m => m.DropoffTime).HasColumnName("tpep_dropoff_datetime");
            entity.Property(m => m.CountOfPassengers).HasColumnName("passenger_count");
            entity.Property(m => m.TripDistance).HasColumnName("trip_distance");
            entity.Property(m => m.StoreAndFwdFlag).HasColumnName("store_and_fwd_flag");
            entity.Property(m => m.PickupLocationID).HasColumnName("PULocationID");
            entity.Property(m => m.DropoffLocationID).HasColumnName("DOLocationID");
            entity.Property(m => m.FareAmount).HasColumnName("fare_amount");
            entity.Property(m => m.TipAmount).HasColumnName("tip_amount");
        });
    }
}

