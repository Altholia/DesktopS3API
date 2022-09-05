using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace DesktopS3API.ParameterDto;

public class GetTransporationTaskCollection
{
    [Display(Name = "出发城市ID")] 
    public int? StartDistrictId { get; set; } = null;

    [Display(Name = "到底城市ID")] 
    public int? DestinationDistrictId { get; set; } = null;

    [Display(Name = "开始时间")] 
    public DateTime? StartDate { get; set; } = null;

    [Display(Name = "结束时间")] 
    public DateTime? ToDate { get; set; } = null;
}