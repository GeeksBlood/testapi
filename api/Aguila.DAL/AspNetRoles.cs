using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Discriminator { get; set; }
        public string Description { get; set; }
        public string Ipaddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
