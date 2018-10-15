using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class CategoryAttributes
    {
        public int Id { get; set; }
        public int? AttributesId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Attributes Attributes { get; set; }
        public virtual SituationCategories Category { get; set; }
    }
}
