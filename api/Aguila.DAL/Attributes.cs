using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class Attributes
    {
        public Attributes()
        {
            CategoryAttributes = new HashSet<CategoryAttributes>();
            SituationAttributes = new HashSet<SituationAttributes>();
        }

        public int Id { get; set; }
        public string AttributeDescr { get; set; }

        public virtual ICollection<CategoryAttributes> CategoryAttributes { get; set; }
        public virtual ICollection<SituationAttributes> SituationAttributes { get; set; }
    }
}
