using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class AssetPhoto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string Photo { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
