using System;
using CodeBase.Logic.Interfaces.General.Factories.Confetti;
using CodeBase.Logic.Interfaces.Scenes.Infinity.Systems.Records;
using CodeBase.Logic.Scenes.Infinity.Providers.Lines;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Infinity.Presenters.Confetti
{
    public class InfinitySceneConfettiEffectPresenter : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IRecordLineProvider _recordLineProvider;
        private readonly IConfettiEffectFactory _confettiEffectFactory;

        public InfinitySceneConfettiEffectPresenter(IInfinityRecordSystem recordSystem,
            IRecordLineProvider recordLineProvider, IConfettiEffectFactory confettiEffectFactory)
        {
            _confettiEffectFactory = confettiEffectFactory;
            _recordLineProvider = recordLineProvider;
            
            _disposable = recordSystem.PlayerRecord.Subscribe(OnPlayerRecordChanged);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private async void OnPlayerRecordChanged(float height)
        {
            if (height == 0f || _recordLineProvider.PlayerRecordLine.Value == null)
            {
                return;
            }

            var position = _recordLineProvider.PlayerRecordLine.Value.GetPosition();
            var effect = await _confettiEffectFactory.SpawnAsync(position, Quaternion.identity);
        }
    }
}