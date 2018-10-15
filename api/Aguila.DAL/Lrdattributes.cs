using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class Lrdattributes
    {
        public Lrdattributes()
        {
            LrdattributesLookups = new HashSet<LrdattributesLookups>();
        }

        public int LrdattributesId { get; set; }
        public string AttributeDescr { get; set; }

        public virtual ICollection<LrdattributesLookups> LrdattributesLookups { get; set; }
    }
}
