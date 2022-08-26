using System;
using System.Collections;
using System.Collections.Generic;
using DesktopS3API.Entities;

namespace DesktopS3API.Models.DisplayDto;

public class AssetDisplayDto
{
    public int Id { get; set; }
    public string AssetNumber { get; set; }
    public string Name { get; set; }
    public string Specification { get; set; }
    public int UpkeepTypeId { get; set; }
    public decimal Price { get; set; }
    public DateTime ManufactureDate { get; set; }
    public DateTime? ServiceDate { get; set; }
    public DateTime RegistrationTime { get; set; }
    public int UpkeepCircle { get; set; }
    public DepartmentDisplayDto Department { get; set; }
    public IEnumerable<UpkeepRecordDisplayDto> UpkeepRecords { get; set; }
}