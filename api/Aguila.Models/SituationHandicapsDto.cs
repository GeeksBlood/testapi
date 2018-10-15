namespace Aguila.Models
{
    public partial class SituationHandicapsDto
    {
        public int Id { get; set; }
        public int SituationId { get; set; }
        public int? NotUsed { get; set; }
        public int HandicapId { get; set; }

        public virtual HandicapsDto NotUsedNavigation { get; set; }
    }
}
