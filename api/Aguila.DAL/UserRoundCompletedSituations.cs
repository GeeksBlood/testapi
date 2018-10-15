using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class UserRoundCompletedSituations
    {
        public UserRoundCompletedSituations()
        {
            UserRoundCompletedQuestions = new HashSet<UserRoundCompletedQuestions>();
        }

        public int Id { get; set; }
        public int Sid { get; set; }
        public int? UserRoundCompletedId { get; set; }
        public decimal Sscore { get; set; }
        public string AspNetUsers_Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime? DateCompleted { get; set; }

        public virtual ICollection<UserRoundCompletedQuestions> UserRoundCompletedQuestions { get; set; }
        public virtual UserRoundCompleteds UserRoundCompleted { get; set; }
    }
}
