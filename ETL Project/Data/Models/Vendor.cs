namespace ETL_Project.Data.Models;

public class Vendor
{
    public int Id { get; set; }
    public DateTime PickupTime { get; set; }
    public DateTime DropoffTime { get; set; }
    public int? CountOfPassengers { get; set; }
    public double TripDistance { get; set; }
    public string StoreAndFwdFlag { get; set; } = null!;
    public decimal FareAmount { get; set; }
    public decimal TipAmount { get; set; }

    public int PickupLocationID { get; set; }
    public int DropoffLocationID { get; set; }
}
