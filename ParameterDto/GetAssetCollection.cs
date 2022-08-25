using System.ComponentModel.DataAnnotations;

namespace DesktopS3API.ParameterDto;

public class GetAssetCollection
{
    [Display(Name="资产ID")]
    public int? AssetId { get; set; }

    [Display(Name="保养类型ID")]
    public int? UpkeepTypeId { get; set; }
}