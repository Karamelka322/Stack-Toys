using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Exceptions;
using Unity.Services.Leaderboards.Models;

namespace CodeBase.Logic.General.Services.Leaderboards
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly AsyncLazy _authenticationTask;
        
        public LeaderboardService()
        {
            _authenticationTask = UniTask.Lazy(AuthenticateAsync);
            _authenticationTask.Task.Forget();
        }

        public async UniTask<PlayerInfo> GetPlayerInfo()
        {
            await _authenticationTask;
         
            return AuthenticationService.Instance.PlayerInfo;
        }
        
        public async UniTask<LeaderboardEntry> AddPlayerScoreAsync(string leaderboardId, float score)
        {
            await _authenticationTask;

            var leaderboardEntry = await GetPlayerScoreAsync(leaderboardId);
            var newScore = leaderboardEntry.Score + score;
            
            return await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, newScore);
        }
        
        public async UniTask<LeaderboardEntry> GetPlayerScoreAsync(string leaderboardId)
        {
            await _authenticationTask;

            try
            {
                return await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
            }
            catch (LeaderboardsException)
            {
                var playerId = AuthenticationService.Instance.PlayerId;
                var playerName = AuthenticationService.Instance.PlayerName;
                
                return new LeaderboardEntry(playerId, playerName, 0, 0);
            }
        }
        
        public async UniTask<LeaderboardScoresPage> GetPageAsync(string leaderboardId, int limit, int offset)
        {
            await _authenticationTask;
            
            var getScoresOptions = new GetScoresOptions()
            {
                Limit = limit,
                Offset = offset,
            };
            
            return await LeaderboardsService.Instance.GetScoresAsync(leaderboardId, getScoresOptions);
        }
        
        private async UniTask AuthenticateAsync()
        {
            UnityServices.InitializeFailed += OnUnityServicesInitializeFailed;
            
            await UnityServices.InitializeAsync();
            
            UnityServices.InitializeFailed -= OnUnityServicesInitializeFailed;
            AuthenticationService.Instance.SignInFailed += OnAuthenticationFailed;
            
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            AuthenticationService.Instance.SignInFailed -= OnAuthenticationFailed;
        }
        
        private void OnUnityServicesInitializeFailed(Exception exception)
        {
            UnityEngine.Debug.LogError(exception.Message);
        }
        
        private void OnAuthenticationFailed(RequestFailedException exception)
        {
            UnityEngine.Debug.LogError(exception.Message);
        }
    }
}