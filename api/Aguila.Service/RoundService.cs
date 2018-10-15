using Aguila.DAL;
using Aguila.Models;
using Aguila.Service.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Service
{
    /// <summary>
    /// This class have interface methods implementation of IRoundService
    /// for Get, Get by ID, Create, Update and Delete User Rounds.
    /// </summary>
    public class RoundService : IRoundService
    {
        private readonly AguilaContext _entities = null;

        public RoundService(AguilaContext entities)
        {
            this._entities = entities;
        }
        public async Task<IEnumerable<UserRoundCompletedsDto>> GetRounds(string userId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRoundCompletedSituations, UserRoundCompletedSituationsDto>();
                cfg.CreateMap<UserRoundCompleteds, UserRoundCompletedsDto>().ForMember(dto => dto.situation,
                           map => map.MapFrom(x => x.UserRoundCompletedSituations));

            });
            IQueryable<UserRoundCompleteds> userRounds = _entities.UserRoundCompleteds.Include(user => user.UserRoundCompletedSituations)
                .Where(user => user.AspNetUsers_Id == userId);
            IEnumerable<UserRoundCompleteds> roundsResult = await userRounds.ToListAsync();
            if (roundsResult != null)
            {
                var mapper = config.CreateMapper();
                IEnumerable<UserRoundCompletedsDto> result = mapper.Map<IEnumerable<UserRoundCompleteds>, IEnumerable<UserRoundCompletedsDto>>(roundsResult);
                return result;
            }
            return null;
        }

        public async Task<UserRoundCompletedsDto> GetRoundsById(int roundId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRoundCompletedSituations, UserRoundCompletedSituationsDto>();
                cfg.CreateMap<UserRoundCompleteds, UserRoundCompletedsDto>().ForMember(dto => dto.situation,
                           map => map.MapFrom(x => x.UserRoundCompletedSituations));

            });

            var rounds = await _entities.UserRoundCompleteds.Include(user => user.UserRoundCompletedSituations)
                 .Where(user => user.Id == roundId).FirstOrDefaultAsync();
            if (rounds != null)
            {
                var mapper = config.CreateMapper();
                UserRoundCompletedsDto result = mapper.Map<UserRoundCompleteds, UserRoundCompletedsDto>(rounds);
                return result;
            }
            return null;
        }

        public async Task<bool> CreateRounds(UserRoundCompletedsDto newRounds)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRoundCompletedQuestionsDto, UserRoundCompletedQuestions>();
                cfg.CreateMap<UserRoundCompletedSituationsDto, UserRoundCompletedSituations>().ForMember(dto2 => dto2.UserRoundCompletedQuestions,
                           map => map.MapFrom(x2 => x2.question));
                cfg.CreateMap<UserRoundCompletedsDto, UserRoundCompleteds>().ForMember(dto => dto.UserRoundCompletedSituations,
                           map => map.MapFrom(x => x.situation));

            });

            var mapper = config.CreateMapper();
            UserRoundCompleteds rounds = mapper.Map<UserRoundCompletedsDto, UserRoundCompleteds>(newRounds);
            _entities.UserRoundCompleteds.Add(rounds);
            try
            {
                await _entities.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> ScoreRound(int id)
        {
            var rounds = await _entities.UserRoundCompleteds.FindAsync(id);
            if (rounds == null)
            {
                return null;
            }

            // process the scoring of the round... situations... questions are passed from front end.
            decimal totalRoundScore = 0;

            // this one gave me everything but we are being more explicit so use this one.
            var myAnswers = await _entities.UserRoundCompleteds
                .Include(x => x.UserRoundCompletedSituations)
                .ThenInclude(round => round.UserRoundCompletedQuestions)
                .Where(x => x.Id == id).ToListAsync();

            // need to calc the situation score...
            // for each round, situation
            decimal situationScore = 0;
            foreach (var item in myAnswers)
            {
                // update the situation score in the database.
                foreach (var sit in item.UserRoundCompletedSituations)
                {
                    decimal newAdditions = 0;
                    foreach (var answers in sit.UserRoundCompletedQuestions)
                    {
                        newAdditions = newAdditions + answers.Qscore;
                        situationScore = situationScore + newAdditions;
                    }
                    totalRoundScore = totalRoundScore + situationScore;
                    sit.Sscore = situationScore;
                }
                item.Rscore = totalRoundScore;
            }
            await _entities.SaveChangesAsync();
            return Convert.ToString(totalRoundScore);
        }

        public async Task<List<UserScoreCardDto>> ScoreMyRounds(string id, int situations1, int situations2, int situations3, int situations4)
        {
            try
            {
                List<UserScoreCardDto> myScoreCard = new List<UserScoreCardDto>();
                DateTime dateNow = DateTime.UtcNow;
                int i = 0;
                decimal totalSituationScore1 = 0;
                decimal totalSituationScore2 = 0;
                decimal totalSituationScore3 = 0;
                decimal totalSituationScore4 = 0;
                decimal averageSituation1 = 0;
                decimal averageSituation2 = 0;
                decimal averageSituation3 = 0;
                decimal averageSituation4 = 0;
                decimal countSituation1 = 0;
                decimal countSituation2 = 0;
                decimal countSituation3 = 0;
                decimal countSituation4 = 0;

                var userSituationsPeriod1 = await _entities.UserRoundCompletedSituations
                   .Where(us => us.AspNetUsers_Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(us => us.DateCompleted)
                    .Take(situations1).ToListAsync();
                var userSituationsPeriod2 = await _entities.UserRoundCompletedSituations
                    .Where(r => r.AspNetUsers_Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(r => r.DateCompleted)
                    .Take(situations2).ToListAsync();
                var userSituationsPeriod3 = await _entities.UserRoundCompletedSituations
                    .Where(r => r.AspNetUsers_Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(r => r.DateCompleted)
                    .Take(situations3).ToListAsync();
                var userSituationsPeriod4 = await _entities.UserRoundCompletedSituations
                    .Where(us => us.AspNetUsers_Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(us => us.DateCompleted)
                    .Take(situations4).ToListAsync();
                i++;
                myScoreCard.Add(new UserScoreCardDto
                {
                    Id = i,
                    Descr = "Number Of Situations:",
                    value1 = situations1,
                    value2 = situations2,
                    value3 = situations3,
                    value4 = situations4
                });

                // get totals
                countSituation1 = userSituationsPeriod1.Count();
                if (countSituation1 == situations1)
                {
                    totalSituationScore1 = userSituationsPeriod1.Sum(x => (decimal?)(x.Sscore)) ?? 0;
                    if (countSituation1 != 0)
                    {
                        averageSituation1 = totalSituationScore1 / countSituation1;
                    };
                }

                countSituation2 = userSituationsPeriod2.Count();
                if (countSituation2 == situations2)
                {
                    totalSituationScore2 = userSituationsPeriod2.Sum(x => (decimal?)(x.Sscore)) ?? 0;
                    if (countSituation2 != 0)
                    {
                        averageSituation2 = totalSituationScore2 / countSituation2;
                    };
                }

                countSituation3 = userSituationsPeriod3.Count();
                if (countSituation3 == situations3)
                {
                    totalSituationScore3 = userSituationsPeriod3.Sum(x => (decimal?)(x.Sscore)) ?? 0;
                    if (countSituation3 != 0)
                    {
                        averageSituation3 = totalSituationScore3 / countSituation3;
                    }
                };
                countSituation4 = userSituationsPeriod4.Count();
                if (countSituation4 == situations4)
                {
                    totalSituationScore4 = userSituationsPeriod4.Sum(x => (decimal?)(x.Sscore)) ?? 0;
                    if (countSituation4 != 0)
                    {
                        averageSituation4 = totalSituationScore4 / countSituation4;
                    }
                };
                i++;
                myScoreCard.Add(new UserScoreCardDto
                {
                    Id = i,
                    Descr = "Total:",
                    value1 = averageSituation1,
                    value2 = averageSituation2,
                    value3 = averageSituation3,
                    value4 = averageSituation4
                });

                // break them out by category after getting totals
                var categories = _entities.SituationCategories;
                decimal totalSituationScore1byCat = 0;
                decimal totalSituationScore2byCat = 0;
                decimal totalSituationScore3byCat = 0;
                decimal totalSituationScore4byCat = 0;
                decimal countSituation1byCat = 0;
                decimal countSituation2byCat = 0;
                decimal countSituation3byCat = 0;
                decimal countSituation4byCat = 0;
                decimal averageSituation1byCat = 0;
                decimal averageSituation2byCat = 0;
                decimal averageSituation3byCat = 0;
                decimal averageSituation4byCat = 0;

                foreach (var item in categories)
                {
                    totalSituationScore1byCat = 0;
                    totalSituationScore2byCat = 0;
                    totalSituationScore3byCat = 0;
                    totalSituationScore4byCat = 0;
                    countSituation1byCat = 0;
                    countSituation2byCat = 0;
                    countSituation3byCat = 0;
                    countSituation4byCat = 0;
                    averageSituation1byCat = 0;
                    averageSituation2byCat = 0;
                    averageSituation3byCat = 0;
                    averageSituation4byCat = 0;

                    // find all Sscores where id matches
                    var userSituationsPeriod1byCat = userSituationsPeriod1
                        .Where(us => us.CategoryId == item.Id);
                    totalSituationScore1byCat = userSituationsPeriod1byCat.Sum(xc => (decimal?)(xc.Sscore)) ?? 0;
                    countSituation1byCat = userSituationsPeriod1byCat.Count();
                    if (countSituation1byCat != 0 && countSituation1byCat == situations1)
                    {
                        averageSituation1byCat = totalSituationScore1byCat / countSituation1byCat;
                    };
                    var userSituationsPeriod2byCat = userSituationsPeriod2
                        .Where(us => us.CategoryId == item.Id);
                    totalSituationScore2byCat = userSituationsPeriod2byCat.Sum(xc => (decimal?)(xc.Sscore)) ?? 0;
                    countSituation2byCat = userSituationsPeriod2byCat.Count();
                    if (countSituation2byCat != 0 && countSituation2byCat == situations2)
                    {
                        averageSituation2byCat = totalSituationScore2byCat / countSituation2byCat;
                    };
                    var userSituationsPeriod3byCat = userSituationsPeriod3
                        .Where(us => us.CategoryId == item.Id);
                    totalSituationScore3byCat = userSituationsPeriod3byCat.Sum(xc => (decimal?)(xc.Sscore)) ?? 0;
                    countSituation3byCat = userSituationsPeriod3byCat.Count();
                    if (countSituation3byCat != 0 && countSituation3byCat == situations3)
                    {
                        averageSituation3byCat = totalSituationScore3byCat / countSituation3byCat;
                    };
                    var userSituationsPeriod4byCat = userSituationsPeriod4
                        .Where(us => us.CategoryId == item.Id);
                    totalSituationScore4byCat = userSituationsPeriod4byCat.Sum(xc => (decimal?)(xc.Sscore)) ?? 0;
                    countSituation4byCat = userSituationsPeriod4byCat.Count();
                    if (countSituation4byCat != 0 && countSituation4byCat == situations4)
                    {
                        averageSituation4byCat = totalSituationScore4byCat / countSituation4byCat;
                    };
                    // write description and total score for that item.
                    i++;
                    myScoreCard.Add(new UserScoreCardDto
                    {
                        Id = i,
                        Descr = item.Description,
                        value1 = averageSituation1byCat,
                        value2 = averageSituation2byCat,
                        value3 = averageSituation3byCat,
                        value4 = averageSituation4byCat
                    });
                }

                return myScoreCard;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }
        }
    }
}
