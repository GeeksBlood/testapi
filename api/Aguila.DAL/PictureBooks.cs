using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class PictureBooks
    {
        public PictureBooks()
        {
            SituationsBook = new HashSet<Situations>();
            SituationsImageFile = new HashSet<Situations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string AndroidUrl { get; set; }

        public virtual ICollection<Situations> SituationsBook { get; set; }
        public virtual ICollection<Situations> SituationsImageFile { get; set; }
    }
}
