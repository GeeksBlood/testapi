using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class Handicaps
    {
        public Handicaps()
        {
            SituationHandicaps = new HashSet<SituationHandicaps>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SituationHandicaps> SituationHandicaps { get; set; }
    }
}
