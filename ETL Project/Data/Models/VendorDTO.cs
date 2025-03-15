namespace ETL_Project.Data.Models;

public class VendorDTO
{
    public DateTime PickupTime { get; set; }
    public DateTime DropoffTime { get; set; }
    public int? CountOfPassengers { get; set; }
    public double TripDistance { get; set; }
    public char? StoreAndFwdFlag { get; set; }
    public decimal FareAmount { get; set; }
    public decimal TipAmount { get; set; }
    public int PickupLocationID { get; set; }
    public int DropoffLocationID { get; set; }
}
