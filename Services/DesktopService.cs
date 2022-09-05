using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesktopS3API.Entities;
using DesktopS3API.ParameterDto;
using Microsoft.EntityFrameworkCore;

namespace DesktopS3API.Services;

public class DesktopService:IDesktopService
{
    private readonly S3Context _context;

    public DesktopService(S3Context context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// 根据用户输入的电话号码和密码查询出对应的 staff 信息
    /// </summary>
    /// <param name="parameter">用户输入的电话号码和密码</param>
    /// <returns>返回 staff 实体信息</returns>
    public async Task<Staff> GetStaffAsync(GetStaff parameter) =>
        await _context.Staff
            .TagWith("根据电话和密码查询 Staff")
            .FirstOrDefaultAsync(r =>
                r.Telephone == parameter.Telephone.Trim() && r.Password == parameter.Password.Trim());

    /// <summary>
    /// 查询出对应的 Asset 以及它所在的部门 Department和他的保养记录
    /// </summary>
    /// <param name="parameter">该参数为过滤条件，可为null，当为null时则不需要进行过滤</param>
    /// <returns>返回 asset 集合</returns>
    public async Task<IEnumerable<Asset>> GetAssetCollectionAsync(GetAssetCollection parameter)
    {
        IQueryable<Asset> linq = _context.Assets.TagWith("查询出所有对应的 Asset 信息") as IQueryable<Asset>;

        if (parameter.AssetId != null)
            linq = linq.Where(r => r.Id == parameter.AssetId);
        if (parameter.UpkeepTypeId != null)
            linq = linq.Where(r => r.UpkeepTypeId == parameter.UpkeepTypeId);
        if (!string.IsNullOrEmpty(parameter.AssetName))
            linq = linq.Where(r => r.Name == parameter.AssetName);
        if (parameter.CategoryId != null)
            linq = linq.Where(r => r.CategoryId == parameter.CategoryId);

        return await linq.Include(r => r.UpkeepRecords)
            .Include(r => r.Department)
            .Include(r => r.Category)
            .ToListAsync();
    }

    /// <summary>
    /// 根据Name字段查询Asset
    /// </summary>
    /// <param name="assetName">Asset的Name字段</param>
    /// <returns>返回查询到的Asset</returns>
    public async Task<Asset> GetAssetByNameAsync(string assetName) =>
        await _context.Assets
            .TagWith("根据Name字段查询Asset")
            .FirstOrDefaultAsync(r => r.Name == assetName);

    /// <summary>
    /// 可以返回不进行过滤的所有的AssetCategory信息
    /// </summary>
    /// <returns>返回进行一系列操作之后的AssetCategory集合</returns>
    public async Task<IEnumerable<AssetCategory>> GetAssetCategoryCollectionAsync() =>
        await _context.AssetCategories
            .TagWith("获取所有的AssetCategory信息")
            .ToListAsync();    
    
    /// <summary>
    /// 根据AssetCategory的Name属性进行搜索
    /// </summary>
    /// <param name="name">AssetCategory的Name字段，进行指定搜索</param>
    /// <returns></returns>
    public async Task<AssetCategory> GetAssetCategoryByNameAsync(string name) =>
        await _context.AssetCategories
            .TagWith("根据AssetCategory的Name属性进行搜索")
            .FirstOrDefaultAsync(r => r.Name == name);

    /// <summary>
    /// 根据保养类型的名称查询出 UpkeepType
    /// </summary>
    /// <param name="upkeepName">保养类型的名称</param>
    /// <returns>返回 upkeepType 实体信息</returns>
    public async Task<UpkeepType> GetUpkeepTypeByNameAsync(string upkeepName) =>
        await _context.UpkeepTypes
            .TagWith("根据保养类型的名称查询出 UpkeepType")
            .FirstOrDefaultAsync(r => r.Name == upkeepName.Trim());

    /// <summary>
    /// 查询出所有的 UpkeepType
    /// </summary>
    /// <returns>返回查询到的 UpkeepType 集合</returns>
    public async Task<IEnumerable<UpkeepType>> GetUpkeepTypeCollectionAsync() =>
        await _context.UpkeepTypes
            .TagWith("查询出所有的 UpkeepType 类型")
            .ToListAsync();

    /// <summary>
    /// 根据 AssetId 查询出对应的 UpkeepRecord 集合
    /// </summary>
    /// <param name="assetId">资产ID</param>
    /// <returns>返回查询到的 UpkeepRecord 集合</returns>
    public async Task<IEnumerable<UpkeepRecord>> GetUpkeepRecordByAssetIdAsync(int assetId) =>
        await _context.UpkeepRecords
            .TagWith("根据 AssetId 查询出对应的 UpkeepRecord 集合")
            .Where(r => r.AssetId==assetId)
            .ToListAsync();

    /// <summary>
    /// 根据Id字段获取对应的Department
    /// </summary>
    /// <param name="departmentId">Department的ID字段</param>
    /// <returns>返回查询到的Department实体信息</returns>
    public async Task<Department> GetDepartmentByIdAsync(int departmentId) =>
        await _context.Departments
            .TagWith("根据departmentId获取Department")
            .FirstOrDefaultAsync(r => r.Id == departmentId);

    /// <summary>
    /// 根据assetId查询AssetTransfer
    /// </summary>
    /// <param name="assetId">资产ID</param>
    /// <returns>返回AssetTransfer集合</returns>
    public async Task<IEnumerable<AssetTransfer>> GetAssetTransferByAssetIdAsync(int assetId) =>
        await _context.AssetTransfers
            .TagWith("根据AssetId查询AssetTransfer")
            .Where(r => r.AssetId == assetId)
            .ToListAsync();

    /// <summary>
    /// 以parameter为过滤条件进行过滤搜索
    /// </summary>
    /// <param name="parameter">过滤条件</param>
    /// <returns>返回过滤之后查询出的TransporationTask集合</returns>
    public async Task<IEnumerable<TransporationTask>> GetTransporationTaskCollectionAsync(
        GetTransporationTaskCollection parameter)
    {
        IQueryable<TransporationTask> linq = _context.TransporationTasks.TagWith("查询TransporationTask");

        if (parameter.StartDistrictId != null)
            linq = linq.Where(r => r.StartDistrictId == parameter.StartDistrictId);
        if (parameter.DestinationDistrictId != null)
            linq = linq.Where(r => r.DestinationDistrictId == parameter.DestinationDistrictId);
        if (parameter.StartDate != null && parameter.ToDate != null)
            linq = linq.Where(r =>
                r.ActualCompletionDate >= parameter.StartDate && r.ActualCompletionDate <= parameter.ToDate);

        return await linq.ToListAsync();
    }

    /// <summary>
    /// 根据CityId查询City
    /// </summary>
    /// <param name="cityId">城市ID，查询条件</param>
    /// <returns>返回city</returns>
    public async Task<City> GetCityByIdAsync(int cityId) =>
        await _context.Cities
            .TagWith("根据CityId查询City")
            .FirstOrDefaultAsync(r => r.Id == cityId);
}