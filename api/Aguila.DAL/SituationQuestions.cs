using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class SituationQuestions
    {
        public SituationQuestions()
        {
            Answers = new HashSet<Answers>();
        }

        public int Id { get; set; }
        public int SituationId { get; set; }
        public string Question { get; set; }
        public int? SituationId1 { get; set; }

        public virtual ICollection<Answers> Answers { get; set; }
        public virtual Situations SituationId1Navigation { get; set; }
    }
}
