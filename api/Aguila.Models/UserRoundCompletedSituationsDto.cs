using System;
using System.Collections.Generic;

namespace Aguila.Models
{
    public partial class UserRoundCompletedSituationsDto
    {
        public int Id { get; set; }
        public int Sid { get; set; }
        public int? UserRoundCompletedId { get; set; }
        public decimal Sscore { get; set; }
        public string AspNetUsers_Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime? DateCompleted { get; set; }

        public virtual ICollection<UserRoundCompletedQuestionsDto> question { get; set; }
    }
}
