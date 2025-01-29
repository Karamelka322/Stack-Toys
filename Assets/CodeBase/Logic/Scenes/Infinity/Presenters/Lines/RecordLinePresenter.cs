using System;
using CodeBase.Data.General.Constants;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Factories.Lines;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records;
using CodeBase.Logic.Scenes.Infinity.Objects.Lines;
using CodeBase.Logic.Scenes.Infinity.Providers.Lines;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Presenters.Lines
{
    public class RecordLinePresenter : IDisposable
    {
        private const float DistanceForHideInformationOnPlayerRecordLine = 0.4f;
        
        private readonly IInfinityRecordSystem _recordSystem;
        private readonly IRecordLineFactory _recordLineFactory;
        private readonly ILevelBorderSystem _levelBorderSystem;
        private readonly IRecordLineProvider _recordLineProvider;
        private readonly CompositeDisposable _compositeDisposable;

        private RecordLine WorldRecordLine
        {
            get => _recordLineProvider.WorldRecordLine.Value;
            set => _recordLineProvider.WorldRecordLine.Value = value;
        }

        private RecordLine PlayerRecordLine
        {
            get => _recordLineProvider.PlayerRecordLine.Value;
            set => _recordLineProvider.PlayerRecordLine.Value = value;
        }

        public RecordLinePresenter(
            IInfinityRecordSystem recordSystem,
            IRecordLineProvider recordLineProvider,
            IRecordLineFactory recordLineFactory,
            IToyTowerHeightObserver toyTowerHeightObserver,
            ILevelBorderSystem levelBorderSystem)
        {
            _recordLineProvider = recordLineProvider;
            _levelBorderSystem = levelBorderSystem;
            _recordLineFactory = recordLineFactory;
            _recordSystem = recordSystem;

            _compositeDisposable = new CompositeDisposable();
            
            recordSystem.IsReady.Subscribe(OnRecordReady).AddTo(_compositeDisposable);
            toyTowerHeightObserver.TowerHeight.Subscribe(OnTowerHeightChanged).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private async void OnRecordReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }

            if (_recordSystem.WorldRecord.Value != 0)
            {
                PlayerRecordLine = await SpawnLineAsync(_recordSystem.PlayerRecord.Value,
                    LocalizationConstants.PlayerRecordLineTitle);
            }
            
            if (_recordSystem.WorldRecord.Value != 0)
            {
                WorldRecordLine = await SpawnLineAsync(_recordSystem.WorldRecord.Value,
                    LocalizationConstants.WorldRecordLineTitle);
            }
            
            _recordSystem.PlayerRecord.Subscribe(OnPlayerRecordChanged).AddTo(_compositeDisposable);
            _recordSystem.WorldRecord.Subscribe(OnWorldRecordChanged).AddTo(_compositeDisposable);

            TryReducePlayerRecordLine();
            TryHidePlayerRecordLine();
        }

        private void OnTowerHeightChanged(float height)
        {
            if (height > _recordSystem.PlayerRecord.Value)
            {
                if (_recordSystem.PlayerRecord.Value >= _recordSystem.WorldRecord.Value)
                {
                    return;
                }
                
                if (PlayerRecordLine != null)
                {
                    PlayerRecordLine.PlayImpulse();
                }
            }
        }
        
        private async void OnPlayerRecordChanged(float height)
        {
            if (height == 0)
                return;
            
            if (PlayerRecordLine == null)
            {
                PlayerRecordLine = await SpawnLineAsync(_recordSystem.PlayerRecord.Value,
                    LocalizationConstants.PlayerRecordLineTitle);
                
                PlayerRecordLine?.PlayShow();
            }
            else
            {
                var position = GetPosition(height);
                
                await PlayerRecordLine.StopImpulseAsync();
                
                PlayerRecordLine.SetPosition(position);
                PlayerRecordLine.SetHeightAsync(height).Forget();
            }
            
            TryReducePlayerRecordLine();
            TryHidePlayerRecordLine();
        }

        private async void OnWorldRecordChanged(float height)
        {
            if (height == 0)
                return;

            if (WorldRecordLine == null)
            {
                WorldRecordLine = await SpawnLineAsync(_recordSystem.WorldRecord.Value,
                    LocalizationConstants.WorldRecordLineTitle);
            }
            else
            {
                var position = GetPosition(height);
                
                WorldRecordLine.SetPosition(position);
                WorldRecordLine.SetHeightAsync(height).Forget();
            }

            if (_recordSystem.WorldRecord.Value <= _recordSystem.PlayerRecord.Value)
            {
                var title = LocalizationConstants.PlayerWorldChampionRecordLineTitle;
                WorldRecordLine.SetTitleAsync(title).Forget();
            }
        }

        private void TryReducePlayerRecordLine()
        {
            if (PlayerRecordLine == null)
            {
                return;
            }

            if (_recordSystem.PlayerRecord.Value >= _recordSystem.WorldRecord.Value)
            {
                return;
            }
            
            var distance = _recordSystem.WorldRecord.Value - DistanceForHideInformationOnPlayerRecordLine;
            
            if (_recordSystem.PlayerRecord.Value >= distance)
            {
                PlayerRecordLine.HideTitle();
                PlayerRecordLine.HideHeight();
                PlayerRecordLine.SetLineAlpha(0.5f);
            }
        }

        private async void TryHidePlayerRecordLine()
        {
            if (PlayerRecordLine == null)
            {
                return;
            }

            if (_recordSystem.PlayerRecord.Value == 0 || 
                _recordSystem.PlayerRecord.Value >= _recordSystem.WorldRecord.Value)
            {
                await PlayerRecordLine.StopImpulseAsync();
                
                PlayerRecordLine.HideTitle();
                PlayerRecordLine.HideHeight();
                PlayerRecordLine.SetLineAlpha(0);
            }
        }

        private async UniTask<RecordLine> SpawnLineAsync(float height, string titleLocalizationId)
        {
            return await _recordLineFactory.SpawnAsync(GetPosition(height), titleLocalizationId, height);
        }

        private Vector3 GetPosition(float record)
        {
            return _levelBorderSystem.OriginPoint + Vector3.up * record;
        }
    }
}