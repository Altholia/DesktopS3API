using System;
using System.Reflection.Metadata.Ecma335;

namespace DesktopS3API.Models.DisplayDto;

public class AssetTransferDisplayDto
{
    public int FromDepartmentId { get; set; }
    public int ToDepartmentId { get; set; }
    public DateTime TransferTime { get; set; }
}