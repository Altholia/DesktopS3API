using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class UpkeepRecord
    {
        public UpkeepRecord()
        {
            PartUsedInUpkeeps = new HashSet<PartUsedInUpkeep>();
        }

        public int Id { get; set; }
        public int AssetId { get; set; }
        public int? Mileage { get; set; }
        public string Remark { get; set; }
        public DateTime UpkeepTime { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual ICollection<PartUsedInUpkeep> PartUsedInUpkeeps { get; set; }
    }
}
