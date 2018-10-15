using System.Collections.Generic;

namespace Aguila.Models
{
    public class NewSituationViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YdsToPin { get; set; }
        public int WindSpeed { get; set; }
        public decimal Elevation { get; set; }
        public string WindDirection { get; set; }
        public int[] AttributesId { get; set; }
        public int SituationCategoryId { get; set; }
        public int[] HandicapId { get; set; }
        public int? NotSituationHandicapId { get; set; }
        public string StartCoordinate { get; set; }
        public string TargetCoordinate { get; set; }
        public int? BookId { get; set; }
        public int? ImageFileId { get; set; }
        public string LineType { get; set; }
        public bool Published { get; set; }
        public bool Unpublished { get; set; }
        public bool Deleted { get; set; }
        public string PinCoordinate { get; set; }
        public bool IsFirstHole { get; set; }
        public int NextHoleSituationId { get; set; }
        public string VoiceOverUrl { get; set; }
        public int? IsBegining { get; set; }

        public virtual ICollection<NewSituationAttributesDto> SituationAttributes { get; set; }
        public virtual ICollection<NewSituationHandicapDto> SituationHandicaps { get; set; }
        public virtual ICollection<NewSituationQuestionsDto> Questions { get; set; }
        public virtual NewPictureBookDto Book { get; set; }
        public virtual NewPictureBookDto ImageFile { get; set; }
        public virtual SituationCategoriesDto SituationCategory { get; set; }

    }

}
