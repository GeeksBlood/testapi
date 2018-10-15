using Aguila.DAL;
using Aguila.Models;
using Aguila.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Service
{
    /// <summary>
    /// This class have interface methods implementation of ISituationService
    /// for Get, Get by ID, Create, Update, Delete, Published and Unpublished Situation.
    /// </summary>
    public class SituationService : ISituationService
    {
        private readonly AguilaContext _context;
        private readonly IMapper _mapper;
        public SituationService(AguilaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SituationsDto>> GetSituation()
        {
            IQueryable<Situations> situations = _context.Situations
                .Include(x => x.SituationHandicaps)
            .Include(x => x.SituationAttributes)
            .Include(x => x.SituationQuestions).ThenInclude(situation => situation.Answers)
            .Include(x => x.SituationCategory)
            .Include(x => x.ImageFile)
            .Include(x => x.Book)
                .Where(x => x.Deleted == false
                && x.Published == true);
            var data = await situations.ToListAsync();
            IEnumerable<SituationsDto> result = _mapper.Map<IEnumerable<Situations>, IEnumerable<SituationsDto>>(data);
            return result;
        }

        public async Task<IEnumerable<SituationsByHoleDto>> GetSituationByHole()
        {
            IQueryable<Situations> situations = _context.Situations
                .Include(x => x.SituationHandicaps)
            .Include(x => x.SituationAttributes)
            .Include(x => x.SituationQuestions).ThenInclude(situation => situation.Answers)
            .Include(x => x.SituationCategory)
            .Include(x => x.ImageFile)
            .Include(x => x.Book)
                .Where(x => x.Deleted == false
                && x.Published == true && x.IsFirstHole == true);
            var data = await situations.ToListAsync();
            IEnumerable<SituationsByHoleDto> result = _mapper.Map<IEnumerable<Situations>, IEnumerable<SituationsByHoleDto>>(data);
            return result;
        }
        public async Task<IEnumerable<SituationsDto>> GetAllSituation()
        {
            try
            {
                IQueryable<Situations> situations = _context.Situations
                    .Include(x => x.SituationHandicaps)
                .Include(x => x.SituationAttributes)
                .Include(x => x.SituationQuestions).ThenInclude(situation => situation.Answers)
                .Include(x => x.SituationCategory)
                .Include(x => x.ImageFile)
                .Include(x => x.Book)
                    .Where(x => x.Deleted == false);
                var data = await situations.ToListAsync();
                IEnumerable<SituationsDto> result = _mapper.Map<IEnumerable<Situations>, IEnumerable<SituationsDto>>(data);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<SituationHoleDto>> GetSituationIsFirstHole()
        {
            IQueryable<Situations> situations = _context.Situations
                .Where(x => x.Deleted == false);
            var data = await situations.ToListAsync();
            IEnumerable<SituationHoleDto> result = _mapper.Map<IEnumerable<Situations>, IEnumerable<SituationHoleDto>>(data);
            return result;
        }

        public async Task<SituationsDto> GetSituationByID(int id)
        {
            Situations situations = await _context.Situations
                .Include(x => x.SituationHandicaps)
            .Include(x => x.SituationAttributes)
            .Include(x => x.SituationQuestions).ThenInclude(situation => situation.Answers)
            .Include(x => x.SituationCategory)
            .Include(x => x.ImageFile)
            .Include(x => x.Book)
                .Where(x => x.Id == id && x.Deleted == false).FirstOrDefaultAsync();
            SituationsDto result = _mapper.Map<Situations, SituationsDto>(situations);
            return result;
        }
        public async Task<IEnumerable<SituationsDto>> GetSituationsByCHA(int C, int[] H, int[] A)
        {
            IQueryable<Situations> situations = _context.Situations
            .Include(x => x.SituationHandicaps)
            .Include(x => x.SituationAttributes)
            .Include(x => x.SituationQuestions).ThenInclude(situation => situation.Answers)
            .Include(x => x.SituationCategory)
            .Include(x => x.ImageFile)
            .Include(x => x.Book)
           .Where(x =>
               x.Deleted == false &&
               x.Published == true &&
               x.SituationCategory.Id == C
               && (A.All(a => x.SituationAttributes.Any(sa => a == sa.AttributesId)) ||
               A.Any(a => x.SituationAttributes.Any(sa => a == sa.AttributesId)))
               && (H.All(a => x.SituationHandicaps.Any(sa => a == sa.HandicapId)) ||
               H.Any(a => x.SituationHandicaps.Any(sa => a == sa.HandicapId)))
           );
            var data = await situations.ToListAsync();
            IEnumerable<SituationsDto> result = _mapper.Map<IEnumerable<Situations>, IEnumerable<SituationsDto>>(data);
            return result;
        }

        public async Task<IEnumerable<SituationCategoriesDto>> GetSituationCategories()
        {
            IQueryable<SituationCategories> situations = _context.SituationCategories;
            var data = await situations.ToListAsync();
            IEnumerable<SituationCategoriesDto> result = _mapper.Map<IEnumerable<SituationCategories>, IEnumerable<SituationCategoriesDto>>(data);
            return result;
        }

        public async Task<EditSituationViewModel> UpdateSituation(int id, EditSituationViewModel newSituation)
        {
            try
            {

                Situations oldSituation = _context.Situations.Where
                    (os => os.Id == id)
                    .Select(os => os).First();
                oldSituation.Deleted = true;
                _context.Entry(oldSituation).State = EntityState.Modified;
                _context.SaveChanges();

                // 2.) insert new situation based on newSituationViewModel 
                Situations situation = new Situations();
                situation.Elevation = Convert.ToString(newSituation.Elevation);
                situation.LineType = newSituation.LineType;
                situation.Name = newSituation.Name;
                situation.PinCoordinate = newSituation.PinCoordinate;
                situation.StartCoordinate = newSituation.StartCoordinate;
                situation.TargetCoordinate = newSituation.TargetCoordinate;
                situation.WindDirection = newSituation.WindDirection;
                situation.WindSpeed = newSituation.WindSpeed;
                situation.IsFirstHole = newSituation.IsFirstHole;
                situation.NextHoleSituationId = newSituation.NextHoleSituationId;
                situation.YdsToPin = newSituation.YdsToPin;
                situation.VoiceOverUrl = newSituation.VoiceOverUrl;
                situation.IsBegining = newSituation.VoiceOverUrl==null?2: newSituation.IsBegining;
                situation.SituationCategory = _context.SituationCategories.Where
                    (c => c.Id == newSituation.SituationCategoryId)
                         .Select(c => c).First();
                situation.Book = _context.PictureBooks.Where
                    (b => b.Id == newSituation.BookId)
                        .Select(b => b).First();
                situation.ImageFile = _context.PictureBooks.Where
                    (i => i.Id == newSituation.ImageFileId)
                        .Select(i => i).First();

                var sid = _context.Situations.Add(situation);  //sid.Id;
                _context.SaveChanges();

                //NewSituationAttributes
                var sa = newSituation.AttributesId.ToList();
                for (int i = 0; i < sa.Count; i++)
                {
                    SituationAttributes situationAttributes = new SituationAttributes();
                    situationAttributes.AttributesId = sa[i];
                    situationAttributes.SituationId = sid.Entity.Id;
                    _context.SituationAttributes.Add(situationAttributes);
                    _context.SaveChanges();
                }

                //NewSituationHandicap
                var sh = newSituation.HandicapId.ToList();
                for (int i = 0; i < sh.Count; i++)
                {
                    SituationHandicaps situationHandicap = new SituationHandicaps();
                    situationHandicap.HandicapId = sh[i];
                    situationHandicap.SituationId = sid.Entity.Id;
                    var h = _context.SituationHandicaps.Add(situationHandicap);
                    _context.SaveChanges();
                }

                //NewSituationQuestions - NewSituationAnswers
                var q = newSituation.Questions.ToList();
                for (int i = 0; i < q.Count; i++)
                {
                    SituationQuestions situationQuestions = new SituationQuestions();
                    situationQuestions.Question = q[i].Question;
                    situationQuestions.SituationId = sid.Entity.Id;
                    situationQuestions.SituationId1 = sid.Entity.Id;
                    var qes = _context.SituationQuestions.Add(situationQuestions);
                    _context.SaveChanges();

                    var a = q[i].Answers.ToList();
                    for (int j = 0; j < a.Count; j++)
                    {
                        Answers answers = new Answers();
                        answers.Answer = a[j].Answer;
                        answers.DidYouKnowLink = a[j].DidYouKnowLink;
                        answers.LessonLink = a[j].LessonLink;
                        answers.Response = a[j].Response;
                        answers.RulesLink = a[j].RulesLink;
                        answers.Score = a[j].Score;
                        answers.SituationQuestionsId = qes.Entity.Id;
                        answers.Qid = qes.Entity.Id;
                        _context.Answers.Add(answers);
                        _context.SaveChanges();
                    }
                }
                newSituation.Id = situation.Id;

                return newSituation;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<NewSituationViewDto> CreateSituation(NewSituationViewDto newSituation)
        {
            try
            {

                if (newSituation != null)
                {
                    Situations situation = new Situations();
                    situation.Name = newSituation.Name;
                    situation.YdsToPin = newSituation.YdsToPin;
                    situation.WindSpeed = newSituation.WindSpeed;
                    situation.Elevation =Convert.ToString(newSituation.Elevation);
                    situation.WindDirection = newSituation.WindDirection;
                    //situation.NotSituationHandicapId = newSituation.NotSituationHandicapId;
                    situation.StartCoordinate = newSituation.StartCoordinate;
                    situation.TargetCoordinate = newSituation.TargetCoordinate;
                    //situation.BookId = newSituation.BookId;
                    //situation.ImageFileId = newSituation.ImageFileId;
                    situation.LineType = newSituation.LineType;
                    situation.PinCoordinate = newSituation.PinCoordinate;
                    situation.IsFirstHole = newSituation.IsFirstHole;
                    situation.NextHoleSituationId = newSituation.NextHoleSituationId;
                    situation.VoiceOverUrl = newSituation.VoiceOverUrl;
                    situation.IsBegining = newSituation.VoiceOverUrl == null ? 2 : newSituation.IsBegining;
                    situation.SituationCategory = _context.SituationCategories.Where
                (c => c.Id == newSituation.SituationCategoryId)
                     .Select(c => c).FirstOrDefault();
                    situation.Book = _context.PictureBooks.Where
                        (b => b.Id == newSituation.BookId)
                            .Select(b => b).FirstOrDefault();
                    situation.ImageFile = _context.PictureBooks.Where
                        (i => i.Id == newSituation.ImageFileId)
                            .Select(i => i).FirstOrDefault();
                    situation.BookId = situation.Book.Id;
                    situation.ImageFileId = situation.ImageFile.Id;
                    var sid = _context.Situations.Add(situation);
                    await _context.SaveChangesAsync();

                    //NewSituationAttributes
                    var sattributes = newSituation.AttributesId.ToList();
                    for (int i = 0; i < sattributes.Count; i++)
                    {
                        SituationAttributes situationAttributes = new SituationAttributes();
                        situationAttributes.AttributesId = sattributes[i];
                        situationAttributes.SituationId = sid.Entity.Id;
                        _context.SituationAttributes.Add(situationAttributes);
                    }
                    await _context.SaveChangesAsync();

                    //NewSituationHandicap

                    if (newSituation.HandicapId != null)
                    {
                        var shandicaps = newSituation.HandicapId.ToList();
                        for (int i = 0; i < shandicaps.Count; i++)
                        {
                            SituationHandicaps situationHandicap = new SituationHandicaps();
                            situationHandicap.HandicapId = shandicaps[i];
                            situationHandicap.SituationId = sid.Entity.Id;
                            var h = _context.SituationHandicaps.Add(situationHandicap);
                        }
                        await _context.SaveChangesAsync();
                    }

                    //NewSituationQuestions - NewSituationAnswers
                    if (newSituation.Questions != null)
                    {
                        var squestions = newSituation.Questions.ToList();
                        for (int i = 0; i < squestions.Count; i++)
                        {
                            SituationQuestions situationQuestions = new SituationQuestions();
                            situationQuestions.Question = squestions[i].Question;
                            situationQuestions.SituationId = sid.Entity.Id;
                            situationQuestions.SituationId1 = sid.Entity.Id;
                            var qestionId = _context.SituationQuestions.Add(situationQuestions);
                            await _context.SaveChangesAsync();
                            var sanswers = squestions[i].Answers.ToList();
                            for (int j = 0; j < sanswers.Count; j++)
                            {
                                Answers answers = new Answers();
                                answers.Answer = sanswers[j].Answer;
                                answers.DidYouKnowLink = sanswers[j].DidYouKnowLink;
                                answers.LessonLink = sanswers[j].LessonLink;
                                answers.Response = sanswers[j].Response;
                                answers.RulesLink = sanswers[j].RulesLink;
                                answers.Score = sanswers[j].Score;
                                answers.SituationQuestionsId = qestionId.Entity.Id;
                                answers.Qid = qestionId.Entity.Id;
                                _context.Answers.Add(answers);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    newSituation.Id = situation.Id;

                    return newSituation;
                }
                return null;

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteSituation(int id)
        {
            Situations situations = await _context.Situations
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (situations != null)
            {
                situations.Deleted = true;
                _context.Entry(situations).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<SituationsDto> PublishSituation(int id)
        {
            Situations situations = await _context.Situations
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (situations != null && situations.Deleted == false)
            {
                situations.Published = true;
                situations.Unpublished = false;
                _context.Entry(situations).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                SituationsDto result = _mapper.Map<Situations, SituationsDto>(situations);
                return result;
            }
            return null;
        }
        public async Task<SituationsDto> UnPublishSituation(int id)
        {
            Situations situations = await _context.Situations
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (situations != null && situations.Deleted == false)
            {
                situations.Published = false;
                situations.Unpublished = true;
                _context.Entry(situations).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                SituationsDto result = _mapper.Map<Situations, SituationsDto>(situations);
                return result;
            }
            return null;
        }
        //code by sohan   Service  

        public async Task<IEnumerable<SituationsDto>> GetSituationsByNextHoleId(int Id)
        {
            using (AguilaContext _context = new AguilaContext())
            {
                _context.Database.OpenConnection();
                DbCommand cmd =  _context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "USP_getSituationsNextHole";
                DbParameter param1 = cmd.CreateParameter();
                param1.ParameterName = "@ID";
                param1.Value = Id;
                cmd.Parameters.Add(param1);
                cmd.CommandType = CommandType.StoredProcedure;
                List<SituationsDto> IistSituation;
                using (var reader = cmd.ExecuteReader())
                {
                    IistSituation = reader.MapToList<SituationsDto>();
                }
                if (IistSituation !=null)
                {
                    IQueryable<Situations> situations = _context.Situations
                    .Include(x => x.SituationHandicaps)
                .Include(x => x.SituationAttributes)
                .Include(x => x.SituationQuestions).ThenInclude(situation => situation.Answers)
                .Include(x => x.SituationCategory)
                    .Where(x => x.Deleted == false);
                    var result1 = situations.Where(x => IistSituation.Any(y => y.Id == x.Id));
                    var data = await result1.ToListAsync();
                    IEnumerable<SituationsDto> result = _mapper.Map<IEnumerable<Situations>, IEnumerable<SituationsDto>>(data);
                    return result;
                }
                else
                {
                    return null;
                }
            }


        }

    }
}
