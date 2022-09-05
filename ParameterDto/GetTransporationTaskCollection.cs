using System.ComponentModel.DataAnnotations;

namespace DesktopS3API.ParameterDto;

public class GetTransporationTaskCollection
{
    [Display(Name="出发城市ID")]
    public int? StartDistrictId { get; set; }

    [Display(Name="到底城市ID")]
    public int? DestinationDistrictId { get; set; }
}