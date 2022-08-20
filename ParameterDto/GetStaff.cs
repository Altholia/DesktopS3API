using System.ComponentModel.DataAnnotations;

namespace DesktopS3API.ParameterDto;

public class GetStaff
{
    [Display(Name="电话号码")]
    [Required(ErrorMessage="{0}不能为空")]
    [StringLength(11,ErrorMessage="{0}的位数必须是{1}",MinimumLength = 11)]
    public string Telephone { get; set; }

    [Display(Name="密码")]
    [Required(ErrorMessage="{0}不能为空")]
    [StringLength(6,ErrorMessage="{0}的位数必须是{1}",MinimumLength = 6)]
    public string Password { get; set; }
}