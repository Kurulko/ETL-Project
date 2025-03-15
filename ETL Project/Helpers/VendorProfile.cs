using AutoMapper;
using ETL_Project.Data.Models;
using ETL_Project.Extentions;

namespace ETL_Project.Services;

public class VendorProfile : Profile
{
    public VendorProfile()
    {
        CreateMap<VendorDTO, Vendor>()
            .ForMember(
                dest => dest.PickupTime,
                opt => opt.MapFrom(src => src.PickupTime.ConvertEstToUtc())
            )
            .ForMember(
                dest => dest.DropoffTime,
                opt => opt.MapFrom(src => src.DropoffTime.ConvertEstToUtc())
            )
            .ForMember(
                dest => dest.StoreAndFwdFlag,
                opt => opt.MapFrom(src => GetFullStoreAndFwdFlagString(src.StoreAndFwdFlag))
            );

        CreateMap<Vendor, VendorDTO>()
           .ForMember(
               dest => dest.StoreAndFwdFlag,
               opt => opt.MapFrom(src => GetShortStoreAndFwdFlagChar(src.StoreAndFwdFlag))
           )
           .ForMember(
               dest => dest.PickupTime,
               opt => opt.MapFrom(src => src.PickupTime.ConvertUtcToEst())
           )
           .ForMember(
               dest => dest.DropoffTime,
               opt => opt.MapFrom(src => src.DropoffTime.ConvertUtcToEst())
           );
    }


    string? GetFullStoreAndFwdFlagString(char? storeAndFwdFlag)
    {
        if (storeAndFwdFlag is null)
            return null;

        return storeAndFwdFlag switch
        {
            'N' => "No",
            'Y' => "Yes",
            _ => throw new ArgumentException("Value must be either 'Y' or 'N'", nameof(storeAndFwdFlag))
        };
    }

    char? GetShortStoreAndFwdFlagChar(string? storeAndFwdFlag)
    {
        if (storeAndFwdFlag is null)
            return null;

        return storeAndFwdFlag switch
        {
            "No" => 'N',
            "Yes" => 'Y',
            _ => throw new ArgumentException("Value must be either 'Yes' or 'No'", nameof(storeAndFwdFlag))
        };
    }
}