using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class UserRoundCompleteds
    {
        public UserRoundCompleteds()
        {
            UserRoundCompletedSituations = new HashSet<UserRoundCompletedSituations>();
        }

        public int Id { get; set; }
        public string AspNetUsers_Id { get; set; }
        public DateTime? DateCompleted { get; set; }
        public decimal Rscore { get; set; }

        public virtual ICollection<UserRoundCompletedSituations> UserRoundCompletedSituations { get; set; }
    }
}
