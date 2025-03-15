using AutoMapper;
using CsvHelper;
using ETL_Project.Data.Models;
using ETL_Project.Services;
using System.Globalization;

namespace ETL_Project.Helpers;

public class VendorWriter
{
    readonly IMapper mapper;
    public VendorWriter(IMapper mapper)
       => this.mapper = mapper;

    public void WriteDataToCsvFile(string filePath, IEnumerable<Vendor> vendors)
    {
        var vendorDTOs = vendors.Select(v => mapper.Map<VendorDTO>(v));

        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<CsvVendorMap>();
        csv.WriteRecords(vendorDTOs);
    }
}
