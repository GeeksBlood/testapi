using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class SituationHandicaps
    {
        public int Id { get; set; }
        public int SituationId { get; set; }
        public int? NotUsed { get; set; }
        public int HandicapId { get; set; }

        public virtual Handicaps NotUsedNavigation { get; set; }
        public virtual Situations Situation { get; set; }
    }
}
