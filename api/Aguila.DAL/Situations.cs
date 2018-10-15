using System;
using System.Collections.Generic;

namespace Aguila.DAL
{
    public partial class Situations
    {
        public Situations()
        {
            SituationAttributes = new HashSet<SituationAttributes>();
            SituationHandicaps = new HashSet<SituationHandicaps>();
            SituationQuestions = new HashSet<SituationQuestions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int YdsToPin { get; set; }
        public int WindSpeed { get; set; }
        public string Elevation { get; set; }
        public string WindDirection { get; set; }
        public int? SituationCategoryId { get; set; }
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
        public virtual ICollection<SituationAttributes> SituationAttributes { get; set; }
        public virtual ICollection<SituationHandicaps> SituationHandicaps { get; set; }
        public virtual ICollection<SituationQuestions> SituationQuestions { get; set; }
        public virtual PictureBooks Book { get; set; }
        public virtual PictureBooks ImageFile { get; set; }
        public virtual SituationCategories SituationCategory { get; set; }
    }
}
