using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class Department
    {
        public Department()
        {
            AssetTransferFromDepartments = new HashSet<AssetTransfer>();
            AssetTransferToDepartments = new HashSet<AssetTransfer>();
            Assets = new HashSet<Asset>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }

        public virtual ICollection<AssetTransfer> AssetTransferFromDepartments { get; set; }
        public virtual ICollection<AssetTransfer> AssetTransferToDepartments { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
