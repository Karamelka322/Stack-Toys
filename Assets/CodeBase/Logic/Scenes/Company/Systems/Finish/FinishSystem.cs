using System;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Finish;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Finish
{
    public class FinishSystem : IDisposable
    {
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly IDisposable _disposable;

        public FinishSystem(ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider, IFinishObserver finishObserver)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;

            _disposable = finishObserver.IsFinished.Subscribe(OnFinishValueChanged);
        }

        private void OnFinishValueChanged(bool isFinish)
        {
            if (isFinish == false)
            {
                return;
            }

            var currentOpenedLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            var nextLevel = _companyLevelsSaveDataProvider.GetNextLevelIndex(currentOpenedLevel);
            
            _companyLevelsSaveDataProvider.SetCompletedLevel(nextLevel);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}