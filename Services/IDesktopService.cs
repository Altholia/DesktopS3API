using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopS3API.Entities;
using DesktopS3API.ParameterDto;

namespace DesktopS3API.Services;

public interface IDesktopService
{
    Task<Staff> GetStaffAsync(GetStaff parameter);
    Task<IEnumerable<Asset>> GetAssetCollectionAsync(GetAssetCollection parameter);
    Task<Asset> GetAssetByNameAsync(string assetName);
    Task<IEnumerable<AssetCategory>> GetAssetCategoryCollectionAsync();
    Task<AssetCategory> GetAssetCategoryByNameAsync(string name);
    Task<UpkeepType> GetUpkeepTypeByNameAsync(string upkeepName);
    Task<IEnumerable<UpkeepType>> GetUpkeepTypeCollectionAsync();
    Task<IEnumerable<UpkeepRecord>> GetUpkeepRecordByAssetIdAsync(int assetId);
    Task<Department> GetDepartmentByIdAsync(int departmentId);
    Task<IEnumerable<AssetTransfer>> GetAssetTransferByAssetIdAsync(int assetId);
    Task<IEnumerable<TransporationTask>> GetTransporationTaskCollectionAsync(GetTransporationTaskCollection parameter);
    Task<City> GetCityByIdAsync(int cityId);
    Task<District> GetDistrictByIdAsync(int id);
    Task<Driver> GetDriverFromStaffAsync(GetDriverFromStaff parameter);
    Task<Vehicle> GetVehicleFromDriverAsync(GetVehicleFromDriver parameter);
}