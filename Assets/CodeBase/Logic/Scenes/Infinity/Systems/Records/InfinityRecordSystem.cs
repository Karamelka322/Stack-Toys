using System;
using System.Linq;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.General.Observers.Toys;
using CodeBase.Logic.General.Services.Leaderboards;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Services.Leaderboards;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Systems.Records
{
    public class InfinityRecordSystem : IInfinityRecordSystem, IDisposable
    {
        private readonly ILeaderboardService _leaderboardService;

        private readonly AsyncLazy _prepareDataTask;
        private readonly IDisposable _disposable;

        public FloatReactiveProperty PlayerRecord { get; }
        public FloatReactiveProperty WorldRecord { get; }
        public BoolReactiveProperty IsReady { get; }

        public InfinityRecordSystem(ILeaderboardService leaderboardService, IToyTowerHeightObserver towerHeightObserver)
        {
            _leaderboardService = leaderboardService;

            _prepareDataTask = UniTask.Lazy(PrepareDataAsync);
            
            PlayerRecord = new FloatReactiveProperty();
            WorldRecord = new FloatReactiveProperty();
            IsReady = new BoolReactiveProperty();
            
            _disposable = towerHeightObserver.TowerHeight.Subscribe(
                height => OnTowerHeightChanged(height).Forget());

            _prepareDataTask.Task.GetAwaiter();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private async UniTask OnTowerHeightChanged(float height)
        {
            await _prepareDataTask;

            var roundHeight = RoundHeight(height);
            
            if (PlayerRecord.Value < roundHeight)
            {
                var score = roundHeight - PlayerRecord.Value;

                await _leaderboardService.AddPlayerScoreAsync(LeaderboardConstants.InfinityModePageId, score);

                PlayerRecord.Value = roundHeight;
            }

            if (PlayerRecord.Value > WorldRecord.Value)
            {
                WorldRecord.Value = PlayerRecord.Value;
            }
        }

        private async UniTask PrepareDataAsync()
        {
            PlayerRecord.Value = await GetPlayerRecordAsync();
            WorldRecord.Value = await GetWorldRecordAsync();
            
            IsReady.Value = true;
        }

        private async UniTask<float> GetWorldRecordAsync()
        {
            var page = await _leaderboardService.GetPageAsync(
                LeaderboardConstants.InfinityModePageId, 1, 0);
            
            return page.Total > 0 ? RoundHeight((float)page.Results.First().Score) : 0f;
        }

        private async UniTask<float> GetPlayerRecordAsync()
        {
            var playerScore = await _leaderboardService.
                GetPlayerScoreAsync(LeaderboardConstants.InfinityModePageId);

            return RoundHeight((float)playerScore.Score);
        }

        private static float RoundHeight(float value)
        {
            return (float)Math.Round(value, 2);
        }
    }
}