using System.Collections.Generic;

namespace Aguila.Models
{
    public class NewSituationQuestionsDto
    {
        public int Id { get; set; }
        public int Situation_Id { get; set; }
        public string Question { get; set; }
        public virtual ICollection<AnswersDto> Answers { get; set; }

    }
}
