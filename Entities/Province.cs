using System;
using System.Collections.Generic;

namespace DesktopS3API.Entities
{
    public partial class Province
    {
        public Province()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
