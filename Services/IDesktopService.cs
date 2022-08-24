using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopS3API.Entities;
using DesktopS3API.ParameterDto;

namespace DesktopS3API.Services;

public interface IDesktopService
{
    Task<Staff> GetStaffAsync(GetStaff parameter);
    Task<IEnumerable<Asset>> GetAssetCollectionAsync(int? upkeepId);
    Task<UpkeepType> GetUpkeepTypeByNameAsync(string upkeepName);
    Task<IEnumerable<UpkeepType>> GetUpkeepTypeCollectionAsync();
    Task<IEnumerable<UpkeepRecord>> GetUpkeepRecordByAssetIdAsync(int assetId);
}