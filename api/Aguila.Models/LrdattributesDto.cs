using System.Collections.Generic;

namespace Aguila.Models
{
    public class LrdattributesDto
    {
        public int LrdattributesId { get; set; }
        public string AttributeDescr { get; set; }
        public ICollection<LrdattributesLookupsDto> LrdattributesLookups { get; set; }

    }
}
