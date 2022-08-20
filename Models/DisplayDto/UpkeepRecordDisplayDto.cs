using System;

namespace DesktopS3API.Models.DisplayDto;

public class UpkeepRecordDisplayDto
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public int? Mileage { get; set; }
    public string Remark { get; set; }
    public DateTime UpkeepTime { get; set; }
}