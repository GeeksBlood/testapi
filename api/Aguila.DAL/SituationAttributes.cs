using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class SituationAttributes
    {
        public int Id { get; set; }
        public int SituationId { get; set; }
        public int AttributesId { get; set; }

        public virtual Attributes Attributes { get; set; }
        public virtual Situations Situation { get; set; }
    }
}
