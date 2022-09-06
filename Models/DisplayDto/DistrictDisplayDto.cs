using DesktopS3API.Entities;

namespace DesktopS3API.Models.DisplayDto;

public class DistrictDisplayDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CityDisplayDto City { get; set; }
}