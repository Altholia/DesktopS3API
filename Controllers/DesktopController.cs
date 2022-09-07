using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesktopS3API.Entities;
using DesktopS3API.Models.DisplayDto;
using DesktopS3API.ParameterDto;
using DesktopS3API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesktopS3API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesktopController : ControllerBase
{
    private readonly ILogger<DesktopController> _logger;
    private readonly IDesktopService _service;
    private readonly IMapper _mapper;

    public DesktopController(ILogger<DesktopController> logger,IDesktopService service,IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// 根据用户输入的电话号码和密码查询 staff 实体信息
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns>返回 staff 实体信息</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("Staff")]
    public async Task<ActionResult<StaffDisplayDto>> GetStaff([FromBody] GetStaff parameter)
    {
        if (parameter == null)
        {
            _logger.LogError("用户输入的电话号码和密码为空");
            throw new ArgumentNullException(nameof(parameter));
        }

        Staff staff = await _service.GetStaffAsync(parameter);
        if (staff == null)
        {
            _logger.LogWarning("该用户不存在");
            return NotFound();
        }

        StaffDisplayDto dto = _mapper.Map<StaffDisplayDto>(staff);
        return Ok(dto);
    }

    /// <summary>
    /// 根据name字段查询对应的 UpkeepType 实体信息
    /// </summary>
    /// <param name="name">保养类型的名称</param>
    /// <returns>返回upkeepType实体信息</returns>
    [HttpGet("UpkeepType/Name")]
    public async Task<ActionResult<UpkeepTypeDisplay>> GetUpkeepTypeByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogError("用户输入的保养类型名称为空");
            return BadRequest("用户输入的保养类型名称为空");
        }

        var upkeep = await _service.GetUpkeepTypeByNameAsync(name);
        if (upkeep == null)
        {
            _logger.LogWarning($"通过 {name} 无法查询到对应的 UpkeepType 实体信息");
            return NotFound();
        }

        var dto = _mapper.Map<UpkeepTypeDisplay>(upkeep);
        return Ok(dto);
    }

    [HttpGet("UpkeepTypes")]
    public async Task<ActionResult<IEnumerable<UpkeepTypeDisplay>>> GetUpkeepTypeCollection()
    {
        var upkeeps = await _service.GetUpkeepTypeCollectionAsync();
        if (!upkeeps.Any())
        {
            _logger.LogWarning("没有查询到任务的 UpkeepType 信息");
            return NotFound();
        }

        var dtoCollection = _mapper.Map<IEnumerable<UpkeepTypeDisplay>>(upkeeps);
        return Ok(dtoCollection);
    }

    /// <summary>
    /// 查询出对应的 asset 以及他所在的部门和上次保养日期
    /// </summary>
    /// <param name="parameter">该参数为过滤条件，可为null，当为null时则不需要进行过滤</param>
    /// <returns>返回 asset 集合</returns>
    [HttpPost("Assets")]
    public async Task<ActionResult<IEnumerable<AssetDisplayDto>>> GetAssetCollection([FromBody] GetAssetCollection parameter)
    {
        if (parameter == null)
        {
            _logger.LogError($"{nameof(GetAssetCollection)}：用户输入的参数为空");
            return BadRequest("用户输入的参数为空");
        }

        IEnumerable<Asset> assetCollection = await _service.GetAssetCollectionAsync(parameter);
        if (!assetCollection.Any())
        {
            _logger.LogWarning("没有对应的 Asset 实体信息");
            return NotFound();
        }

        var dtoCollection = _mapper.Map<IEnumerable<AssetDisplayDto>>(assetCollection);
        return Ok(dtoCollection);
    }

    [HttpGet("Asset")]
    public async Task<ActionResult<Asset>> GetAssetByName([FromQuery] string assetName)
    {
        if (string.IsNullOrEmpty(assetName))
        {
            _logger.LogError($"{nameof(GetAssetByName)}：查询字符串assetName为空");
            return BadRequest("查询字符串assetName为空");
        }

        Asset asset = await _service.GetAssetByNameAsync(assetName);
        if (asset == null)
        {
            _logger.LogWarning("无法通过Name字段查询到Asset");
            return NotFound();
        }

        AssetDisplayDto dto = _mapper.Map<AssetDisplayDto>(asset);
        return Ok(dto);
    }

    
    /// <summary>
    /// 根据 AssetId 查询出对应的 UpkeepRecord 保养记录
    /// </summary>
    /// <param name="assetId">资产ID</param>
    /// <returns>返回查询到的 UpkeepRecord 集合</returns>
    [HttpGet("UpkeepRecord/AssetId")]
    public async Task<ActionResult<IEnumerable<UpkeepRecordDisplayDto>>> GetUpkeepRecordCollection(
        [FromQuery] int assetId)
    {
        var upkeepRecords = await _service.GetUpkeepRecordByAssetIdAsync(assetId);
        if (!upkeepRecords.Any())
        {
            _logger.LogWarning("没有查询到任何数据");
            return NotFound();
        }

        var dtoCollection = _mapper.Map<IEnumerable<UpkeepRecordDisplayDto>>(upkeepRecords);
        return Ok(dtoCollection);
    }

    /// <summary>
    /// 获取所有的AssetCategory信息
    /// </summary>
    /// <returns>返回集合</returns>
    [HttpGet("AssetCategories")]
    public async Task<ActionResult<IEnumerable<AssetCategoryDisplayDto>>> GetAssetCategoryCollection()
    {
        var entities = await _service.GetAssetCategoryCollectionAsync();
        if (!entities.Any())
        {
            _logger.LogWarning("没有查询到与 AssetCategory 相关的信息");
            return NotFound();
        }

        var dtoCollection = _mapper.Map<IEnumerable<AssetCategoryDisplayDto>>(entities);
        return Ok(dtoCollection);
    }

    /// <summary>
    /// 根据AssetCategory的Name属性返回指定的AssetCategory信息
    /// </summary>
    /// <param name="name">查询条件</param>
    /// <returns>返回查询到的信息</returns>
    [HttpGet("AssetCategory/{name}")]
    public async Task<ActionResult<AssetCategoryDisplayDto>> GetAssetCategoryByName([FromRoute(Name = "Name")] string name)
    {
        if (name == null)
        {
            _logger.LogError($"{nameof(GetAssetCategoryByName)}：name查询条件为空");
            return BadRequest("查询条件为空");
        }

        AssetCategory assetCategory = await _service.GetAssetCategoryByNameAsync(name);
        if (assetCategory == null)
        {
            _logger.LogWarning($"{nameof(GetAssetCategoryByName)}：没有找到相关的实体信息");
            return NotFound();
        }

        var dto = _mapper.Map<AssetCategoryDisplayDto>(assetCategory);
        return Ok(dto);
    }

    /// <summary>
    /// 根据Department的Id字段查询相关的Department信息
    /// </summary>t
    /// <param name="departmentId">Department的ID字段</param>
    /// <returns>返回查询到的Department信息</returns>
    [HttpGet("Department/{departmentId:int}")]
    public async Task<ActionResult<DepartmentDisplayDto>> GetDepartmentById([FromRoute] int departmentId)
    {
        Department department = await _service.GetDepartmentByIdAsync(departmentId);
        if (department == null)
        {
            _logger.LogWarning($"{nameof(GetDepartmentById)}：无法查询到相关的Department信息");
            return NotFound();
        }

        DepartmentDisplayDto dto = _mapper.Map<DepartmentDisplayDto>(department);
        return Ok(dto);
    }

    /// <summary>
    /// 根据assetId查询AssetTransfer
    /// </summary>
    /// <param name="assetId">资产ID</param>
    /// <returns>返回AssetTransferDisplayDto</returns>
    [HttpGet("AssetTransfers")]
    public async Task<ActionResult<AssetTransferDisplayDto>> GetAssetTransferByAssetId([FromQuery] int assetId)
    {
        IEnumerable<AssetTransfer> assetTransfers = await _service.GetAssetTransferByAssetIdAsync(assetId);
        if (assetTransfers == null)
        {
            _logger.LogWarning($"{nameof(GetAssetTransferByAssetId)}：无法根据assetId查询AssetTransfer");
            return NotFound();
        }

        IEnumerable<AssetTransferDisplayDto> dtoCollection = 
            _mapper.Map<IEnumerable<AssetTransferDisplayDto>>(assetTransfers);
        return Ok(dtoCollection);
    }

    /// <summary>
    /// 根据assetId查询UpkeepRecord
    /// </summary>
    /// <param name="assetId">资产ID</param>
    /// <returns>返回UpkeepRecord集合</returns>
    [HttpGet("UpkeepRecords")]
    public async Task<ActionResult<UpkeepRecordDisplayDto>> GetUpkeepRecordCollectionByAssetId([FromQuery] int assetId)
    {
        IEnumerable<UpkeepRecord> upkeepRecords = await _service.GetUpkeepRecordByAssetIdAsync(assetId);
        if (!upkeepRecords.Any())
        {
            _logger.LogWarning($"{nameof(GetUpkeepRecordCollectionByAssetId)}：无法通过assetId查询UpkeepRecord");
            return NotFound();
        }

        IEnumerable<UpkeepRecordDisplayDto> dtoCollection =
            _mapper.Map<IEnumerable<UpkeepRecordDisplayDto>>(upkeepRecords);

        return Ok(dtoCollection);
    }

    /// <summary>
    /// 以parameter为过滤条件进行过滤搜索
    /// </summary>
    /// <param name="parameter">过滤条件</param>
    /// <returns>返回查询到的TransporationTask集合</returns>
    [HttpPost("TransporationTask")]
    public async Task<ActionResult<TransporationTaskDisplay>> GetTransporationTaskCollection(
        [FromBody] GetTransporationTaskCollection parameter)
    {
        if (parameter == null)
        {
            _logger.LogError("用户输入的参数为空");
            return BadRequest();
        }

        IEnumerable<TransporationTask> transporationTasks = await _service.GetTransporationTaskCollectionAsync(parameter);
        if (transporationTasks == null||!transporationTasks.Any())
        {
            _logger.LogWarning($"{nameof(GetTransporationTaskCollection)}：没有数据");
            return NotFound();
        }

        IEnumerable<TransporationTaskDisplay> dtoCollection =
            _mapper.Map<IEnumerable<TransporationTaskDisplay>>(transporationTasks);
        return Ok(dtoCollection);
    }

    /// <summary>
    /// 根据CityId查询City
    /// </summary>
    /// <param name="cityId">城市ID</param>
    /// <returns>返回City</returns>
    [HttpGet("City/{id:int}")]
    public async Task<ActionResult<CityDisplayDto>> GetCityById([FromRoute(Name = "Id")] int cityId)
    {
        City city = await _service.GetCityByIdAsync(cityId);
        if (city == null)
        {
            _logger.LogWarning($"{nameof(GetCityById)}：无法通过cityId查询出对应的City");
            return NotFound();
        }

        CityDisplayDto dto = _mapper.Map<CityDisplayDto>(city);
        return Ok(dto);
    }

    /// <summary>
    /// 根据id查询District
    /// </summary>
    /// <param name="id">District的ID字段</param>
    /// <returns>返回查询到的District</returns>
    [HttpGet("District/{id:int}")]
    public async Task<ActionResult<DistrictDisplayDto>> GetDistrictById([FromRoute(Name = "Id")] int id)
    {
        District district = await _service.GetDistrictByIdAsync(id);
        if (district == null)
        {
            _logger.LogWarning($"{nameof(GetDistrictById)}：无法通过Id查询出对应的District");
            return NotFound();
        }

        DistrictDisplayDto dto = _mapper.Map<DistrictDisplayDto>(district);
        return Ok(dto);
    }

    /// <summary>
    /// 根据Staff的Name查询出Driver
    /// </summary>
    /// <param name="parameter">Staff的Name，由FirstName和LastName组合</param>
    /// <returns>返回Driver</returns>
    [HttpPost("Driver/Staff/Name")]
    public async Task<ActionResult<Driver>> GetDriverFromStaff([FromBody] GetDriverFromStaff parameter)
    {
        Driver driver = await _service.GetDriverFromStaffAsync(parameter);
        if (driver == null)
        {
            _logger.LogWarning($"{nameof(GetDriverFromStaff)}：无法通过staffName查询出Driver");
            return NotFound();
        }

        DriverDisplayDto dto = _mapper.Map<DriverDisplayDto>(driver);
        return Ok(dto);
    }

    /// <summary>
    /// 根据Driver的ID查询Vehicle
    /// </summary>
    /// <param name="parameter">过滤条件</param>
    /// <returns>返回Vehicle</returns>
    [HttpPost("Vehicle/Driver/Id")]
    public async Task<ActionResult<Vehicle>> GetVehicleFromDriver([FromBody] GetVehicleFromDriver parameter)
    {
        if (parameter == null)
        {
            _logger.LogError($"{nameof(GetVehicleFromDriver)}：过滤条件为空");
            return BadRequest("过滤条件为空");
        }

        Vehicle vehicle = await _service.GetVehicleFromDriverAsync(parameter);
        if (vehicle == null)
        {
            _logger.LogWarning($"{nameof(GetVehicleFromDriver)}：无法通过DriverId查询Vehicle");
            return NotFound();
        }

        VehicleDisplayDto dto = _mapper.Map<VehicleDisplayDto>(vehicle);
        return Ok(dto);
    }
}