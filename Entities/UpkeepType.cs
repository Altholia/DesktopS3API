using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class UpkeepType
    {
        public UpkeepType()
        {
            Assets = new HashSet<Asset>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
