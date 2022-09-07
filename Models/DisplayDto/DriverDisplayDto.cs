using System;
using System.Reflection.Metadata.Ecma335;

namespace DesktopS3API.Models.DisplayDto;

public class DriverDisplayDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}