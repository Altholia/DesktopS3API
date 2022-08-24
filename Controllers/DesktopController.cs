using System;
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
    /// <param name="upkeepId">该参数为过滤条件，可为null，当为null时则不需要进行过滤</param>
    /// <returns>返回 asset 集合</returns>
    [HttpGet("Asset/{upkeepId:int?}")]
    public async Task<ActionResult<IEnumerable<AssetDisplayDto>>> GetAssetCollection([FromRoute] int? upkeepId)
    {
        IEnumerable<Asset> assetCollection = await _service.GetAssetCollectionAsync(upkeepId);
        if (!assetCollection.Any())
        {
            _logger.LogWarning("没有对应的 Asset 实体信息");
            return NotFound();
        }

        var dtoCollection = _mapper.Map<IEnumerable<AssetDisplayDto>>(assetCollection);
        return Ok(dtoCollection);
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
}