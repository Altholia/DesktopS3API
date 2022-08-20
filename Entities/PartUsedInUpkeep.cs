using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class PartUsedInUpkeep
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int UpkeepRecordId { get; set; }
        public int Amount { get; set; }

        public virtual Part Part { get; set; }
        public virtual UpkeepRecord UpkeepRecord { get; set; }
    }
}
