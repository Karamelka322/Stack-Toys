using System;
using CodeBase.Logic.Interfaces.General.Factories.Confetti;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Lines;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Presenters.Confetti
{
    public class CompanySceneConfettiEffectPresenter : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly IFinishLineProvider _finishLineProvider;
        private readonly IConfettiEffectFactory _confettiEffectFactory;

        public CompanySceneConfettiEffectPresenter(IFinishObserver finishObserver,
            IFinishLineProvider finishLineProvider, IConfettiEffectFactory confettiEffectFactory)
        {
            _confettiEffectFactory = confettiEffectFactory;
            _finishLineProvider = finishLineProvider;

            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private async void OnFinishValueChanged(bool isFinished)
        {
            if (isFinished == false)
            {
                return;
            }

            await _confettiEffectFactory.SpawnAsync(_finishLineProvider.Line.Value.GetPosition());
        }
    }
}