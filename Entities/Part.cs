using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class Part
    {
        public Part()
        {
            PartUsedInUpkeeps = new HashSet<PartUsedInUpkeep>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public decimal? Price { get; set; }
        public int MinInventory { get; set; }
        public int MaxInventory { get; set; }

        public virtual PartCategory Category { get; set; }
        public virtual ICollection<PartUsedInUpkeep> PartUsedInUpkeeps { get; set; }
    }
}
