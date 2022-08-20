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

        CreateMap<Asset, AssetDisplayDto>();
        CreateMap<Department, DepartmentDisplayDto>();
        CreateMap<UpkeepType, UpkeepTypeDisplay>();
        CreateMap<UpkeepRecord, UpkeepRecordDisplayDto>();
    }
}