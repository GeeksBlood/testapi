using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Models
{
    public partial class PictureBooksAdminDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string type { get; set; }
        public string imageUrl { get; set; }
        public string androidUrl { get; set; }
    }
}
