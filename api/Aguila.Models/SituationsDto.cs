using System.Collections.Generic;

namespace Aguila.Models
{
    public partial class SituationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YdsToPin { get; set; }
        public int WindSpeed { get; set; }
        public string Elevation { get; set; }
        public string WindDirection { get; set; }
        public string StartCoordinate { get; set; }
        public string TargetCoordinate { get; set; }
        public string LineType { get; set; }
        public bool Published { get; set; }
        public bool Unpublished { get; set; }
        public bool Deleted { get; set; }
        public string PinCoordinate { get; set; }
        public bool IsFirstHole { get; set; }
        public int NextHoleSituationId { get; set; }
        public string VoiceOverUrl { get; set; }
        public int? IsBegining { get; set; }

        public  ICollection<SituationAttributesDto> SituationAttributes { get; set; }
        public  ICollection<SituationHandicapsDto> SituationHandicaps { get; set; }
        public  ICollection<SituationQuestionsDto> SituationQuestions { get; set; }
        public  PictureBooksDto Book { get; set; }
        public  PictureBooksDto ImageFile { get; set; }
        public  SituationCategoriesDto SituationCategory { get; set; }
    }

    public class EditSituationViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ImageFileId { get; set; }
        public int[] AttributesId { get; set; }
        public int SituationCategoryId { get; set; }
        public int[] HandicapId { get; set; }
        public string Name { get; set; }
        public int YdsToPin { get; set; }
        public decimal Elevation { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public string StartCoordinate { get; set; }
        public string TargetCoordinate { get; set; }
        public string PinCoordinate { get; set; }
        public string LineType { get; set; }
        public bool IsFirstHole { get; set; }
        public int NextHoleSituationId { get; set; }
        public string VoiceOverUrl { get; set; }
        public int? IsBegining { get; set; }
        public virtual ICollection<NewSituationQuestionsDto> Questions { get; set; }

    }
}
