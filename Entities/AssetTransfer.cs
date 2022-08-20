using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class AssetTransfer
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int FromDepartmentId { get; set; }
        public int ToDepartmentId { get; set; }
        public DateTime TransferTime { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Department FromDepartment { get; set; }
        public virtual Department ToDepartment { get; set; }
    }
}
