using AutoMapper;
using DesktopS3API.Entities;
using DesktopS3API.Models.DisplayDto;

namespace DesktopS3API.Profiles;

public class Profiles:Profile
{
    public Profiles()
    {
        CreateMap<Staff, StaffDisplayDto>()
            .ForMember(des => des.Name, res => res.MapFrom(r => $"{r.FirstName} {r.LastName}"))
            .ForMember(des => des.Sex, res => res.MapFrom(r => r.Gender == "M" ? "男" : "女"));

        CreateMap<TransporationTask, TransporationTaskDisplay>()
            .ForMember(des => des.Staff, res => res.MapFrom(r => r.VehicleTeamAdministratorNavigation));

        CreateMap<Driver, DriverDisplayDto>()
            .ForMember(des => des.Name, res => res.MapFrom(r => $"{r.FirstName} {r.LastName}"))
            .ForMember(des => des.Gender, res => res.MapFrom(r => r.Gender == "F" ? "女" : "男"))
            .ForMember(des => des.DateOfBirth, res => res.MapFrom(r => r.DateofBirth));

        CreateMap<Asset, AssetDisplayDto>();
        CreateMap<Department, DepartmentDisplayDto>();
        CreateMap<UpkeepType, UpkeepTypeDisplay>();
        CreateMap<UpkeepRecord, UpkeepRecordDisplayDto>();
        CreateMap<AssetCategory, AssetCategoryDisplayDto>();
        CreateMap<AssetTransfer, AssetTransferDisplayDto>();
        CreateMap<City, CityDisplayDto>();
        CreateMap<District, DistrictDisplayDto>();
        CreateMap<Vehicle, VehicleDisplayDto>();
    }
}