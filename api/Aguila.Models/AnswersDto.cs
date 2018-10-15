namespace Aguila.Models
{
    public partial class AnswersDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Response { get; set; }
        public decimal Score { get; set; }
        public string RulesLink { get; set; }
        public string LessonLink { get; set; }
        public string DidYouKnowLink { get; set; }
        public int qid { get; set; }
    }
}
