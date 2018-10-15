using System;
using System.Collections.Generic;

namespace Aguila.Models
{
    public partial class UserRoundCompletedsDto
    {
        public int Id { get; set; }
        public string AspNetUsers_Id { get; set; }
        public DateTime? DateCompleted { get; set; }
        public decimal Rscore { get; set; }

        public virtual ICollection<UserRoundCompletedSituationsDto> situation { get; set; }
    }
}
