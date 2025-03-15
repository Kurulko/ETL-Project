using ETL_Project.Data.Models;
using ETL_Project.Helpers;
namespace ETL_Project.Services;

public class VendorImporter
{
    readonly CsvSettings csvSettings;
    readonly VendorReader vendorReader;
    readonly VendorWriter vendorWriter;
    readonly VendorRepository vendorRepository;
    public VendorImporter(CsvSettings csvSettings, VendorReader vendorReader, VendorWriter vendorWriter, VendorRepository vendorRepository)
    {
        this.csvSettings = csvSettings;
        this.vendorReader = vendorReader;
        this.vendorWriter = vendorWriter;
        this.vendorRepository = vendorRepository;
    }

    public async Task ImportCsvDataAsync()
    {
        var vendors = vendorReader.ReadCsvData(csvSettings.DataFilePath);

        var duplicateRecords = RemoveDuplicateRecords(ref vendors);
        vendorWriter.WriteDataToCsvFile(csvSettings.DuplicatesFilePath, duplicateRecords);

        await vendorRepository.AddVendorsAsync(vendors);
    }

    /// <summary>
    /// Remove duplicate records from vendors
    /// </summary>
    /// <param name="vendors"></param>
    /// <returns>Duplicate records</returns>
    IEnumerable<Vendor> RemoveDuplicateRecords(ref IEnumerable<Vendor> vendors)
    {
        var distinctVendors = vendors.DistinctBy(v => (v.PickupTime, v.DropoffTime, v.CountOfPassengers));
        var duplicateVendors = vendors.Except(distinctVendors);

        vendors = distinctVendors;
        return duplicateVendors;
    }
}
