using CsvHelper.Configuration;
using ETL_Project.Data.Models;

namespace ETL_Project.Services;

public sealed class CsvVendorMap : ClassMap<VendorDTO>
{
    public CsvVendorMap()
    {
        Map(m => m.PickupTime).Name("tpep_pickup_datetime");
        Map(m => m.DropoffTime).Name("tpep_dropoff_datetime");
        Map(m => m.CountOfPassengers).Name("passenger_count");
        Map(m => m.TripDistance).Name("trip_distance");
        Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag");
        Map(m => m.PickupLocationID).Name("PULocationID");
        Map(m => m.DropoffLocationID).Name("DOLocationID");
        Map(m => m.FareAmount).Name("fare_amount");
        Map(m => m.TipAmount).Name("tip_amount");
    }
}