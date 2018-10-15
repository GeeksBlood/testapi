using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class Lrdcategories
    {
        public int LrdcategoryId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? LrddataLrddataId { get; set; }

        public virtual Lrddatas LrddataLrddata { get; set; }
    }
}
