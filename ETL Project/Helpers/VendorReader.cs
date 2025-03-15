using AutoMapper;
using CsvHelper;
using ETL_Project.Data.Models;
using System.Globalization;

namespace ETL_Project.Services;

public class VendorReader
{
    readonly IMapper mapper;
    public VendorReader(IMapper mapper)
       => this.mapper = mapper;

    public IEnumerable<Vendor> ReadCsvData(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<CsvVendorMap>();
        var vendorDTOs = csv.GetRecords<VendorDTO>();

        return vendorDTOs.Select(v => mapper.Map<Vendor>(v)).ToList();
    }
}
