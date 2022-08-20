using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class AssetCategory
    {
        public AssetCategory()
        {
            Assets = new HashSet<Asset>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
