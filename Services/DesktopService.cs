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

        return await linq.Include(r=>r.UpkeepRecords).Include(r=>r.Department).ToListAsync();
    }

    /// <summary>
    /// 查询出所有的 AssetCategory 信息
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<AssetCategory>> GetAssetCategoryCollectionAsync() =>
        await _context.AssetCategories
            .TagWith("查询所有的 AssetCategory 信息")
            .ToListAsync();

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
}