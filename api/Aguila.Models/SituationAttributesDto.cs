namespace Aguila.Models
{
    public partial class SituationAttributesDto
    {
        public int Id { get; set; }
        public int SituationId { get; set; }
        public int AttributesId { get; set; }

        public virtual AttributesDto Attributes { get; set; }
    }
}
