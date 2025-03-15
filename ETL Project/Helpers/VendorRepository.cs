using ETL_Project.Data;
using ETL_Project.Data.Models;
using EFCore.BulkExtensions;

namespace ETL_Project.Services;

public class VendorRepository
{
    readonly ETLContext db;
    public VendorRepository(ETLContext db)
        => this.db = db;

    public async Task AddVendorsAsync(IEnumerable<Vendor> vendors)
    {
        await db.BulkInsertAsync(vendors.ToList());
        //await db.Vendors.AddRangeAsync(vendors);
        //await db.SaveChangesAsync();
    }
}
