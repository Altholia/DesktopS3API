using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace DesktopS3API.ParameterDto;

public class GetDriverFromStaff
{
    [Display(Name="员工姓名")]
    [Required(ErrorMessage = "{0}不能为空")]
    public string StaffName { get; set; }
}