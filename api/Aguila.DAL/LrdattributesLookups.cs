using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class LrdattributesLookups
    {
        public int Id { get; set; }
        public int LrdattributesId { get; set; }
        public int LrdcategoryId { get; set; }
        public int? LrddataLrddataId { get; set; }

        public virtual Lrdattributes Lrdattributes { get; set; }
        public virtual Lrddatas LrddataLrddata { get; set; }
    }
}
