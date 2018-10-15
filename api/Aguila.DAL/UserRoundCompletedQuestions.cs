using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class UserRoundCompletedQuestions
    {
        public int Id { get; set; }
        public int Qid { get; set; }
        public int Aid { get; set; }
        public int? UserRoundCompletedSituationsId { get; set; }
        public decimal Qscore { get; set; }

        public virtual UserRoundCompletedSituations UserRoundCompletedSituations { get; set; }
    }
}
