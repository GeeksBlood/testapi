using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class SituationCategories
    {
        public SituationCategories()
        {
            CategoryAttributes = new HashSet<CategoryAttributes>();
            Situations = new HashSet<Situations>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CategoryAttributes> CategoryAttributes { get; set; }
        public virtual ICollection<Situations> Situations { get; set; }
    }
}
