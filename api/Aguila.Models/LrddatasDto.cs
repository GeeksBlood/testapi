using System.Collections.Generic;

namespace Aguila.Models
{
    public partial class LrddatasDto
    {
        public int LrddataId { get; set; }
        public string Lrdtype { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string VideoUrl { get; set; }
        public string ContentLocation { get; set; }

        public virtual ICollection<LrdattributesLookupsDto> LrdattributesLookups { get; set; }
        public virtual ICollection<LrdcategoriesDto> Lrdcategories { get; set; }
    }
}
