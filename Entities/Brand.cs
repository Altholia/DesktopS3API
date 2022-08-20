using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class Brand
    {
        public Brand()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
