using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class Asset
    {
        public Asset()
        {
            AssetPhotos = new HashSet<AssetPhoto>();
            AssetTransfers = new HashSet<AssetTransfer>();
            UpkeepRecords = new HashSet<UpkeepRecord>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string AssetNumber { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public int DepartmentId { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime? ServiceDate { get; set; }
        public DateTime RegistrationTime { get; set; }
        public int? UpkeepTypeId { get; set; }
        public int? UpkeepCircle { get; set; }

        public virtual AssetCategory Category { get; set; }
        public virtual Department Department { get; set; }
        public virtual UpkeepType UpkeepType { get; set; }
        public virtual ICollection<AssetPhoto> AssetPhotos { get; set; }
        public virtual ICollection<AssetTransfer> AssetTransfers { get; set; }
        public virtual ICollection<UpkeepRecord> UpkeepRecords { get; set; }
    }
}
