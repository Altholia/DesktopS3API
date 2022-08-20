using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class PartCategory
    {
        public PartCategory()
        {
            Parts = new HashSet<Part>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}
