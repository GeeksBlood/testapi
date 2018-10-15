using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class Answers
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Response { get; set; }
        public decimal Score { get; set; }
        public string RulesLink { get; set; }
        public string LessonLink { get; set; }
        public string DidYouKnowLink { get; set; }
        public int? SituationQuestionsId { get; set; }
        public int Qid { get; set; }

        public virtual SituationQuestions SituationQuestions { get; set; }
    }
}
