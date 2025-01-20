using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards.Models;

namespace CodeBase.Logic.General.Services.Leaderboards
{
    public interface ILeaderboardService
    {
        UniTask<PlayerInfo> GetPlayerInfo();
        UniTask<LeaderboardEntry> AddPlayerScoreAsync(string leaderboardId, float score);
        UniTask<LeaderboardEntry> GetPlayerScoreAsync(string leaderboardId);
        UniTask<LeaderboardScoresPage> GetPageAsync(string leaderboardId, int limit, int offset);
    }
}