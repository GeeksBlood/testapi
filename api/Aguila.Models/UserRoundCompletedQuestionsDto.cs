namespace Aguila.Models
{
    public partial class UserRoundCompletedQuestionsDto
    {
        public int Id { get; set; }
        public int Qid { get; set; }
        public int Aid { get; set; }
        public int? UserRoundCompletedSituationsId { get; set; }
        public decimal Qscore { get; set; }

        public virtual UserRoundCompletedSituationsDto UserRoundCompletedSituations { get; set; }

        
    }
}
