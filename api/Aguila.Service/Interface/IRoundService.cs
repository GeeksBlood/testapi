using Aguila.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Service.Interface
{
    /// <summary>
    /// Interface for the User Round service class.
    /// </summary>
    public interface IRoundService
    {
        Task<IEnumerable<UserRoundCompletedsDto>> GetRounds(string userId);
        Task<UserRoundCompletedsDto> GetRoundsById(int roundId);
        Task<bool> CreateRounds(UserRoundCompletedsDto newRounds);
        Task<string> ScoreRound(int id);
        Task<List<UserScoreCardDto>> ScoreMyRounds(string id, int situations1, int situations2, int situations3, int situations4);
    }
}
