using System.Collections.Generic;

namespace Aguila.Models
{
    public partial class SituationQuestionsDto
    {
        public int Id { get; set; }
        public int SituationId { get; set; }
        public string Question { get; set; }
        public int? SituationId1 { get; set; }

        public virtual ICollection<AnswersDto> Answers { get; set; }
    }
}
