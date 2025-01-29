using System;
using System.Threading;
using CodeBase.Logic.General.Formulas;
using CodeBase.Logic.General.Systems.Levels;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.Scenes.Company.Providers.Objects.Levels;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class CompanyLevelSizeSystem : BaseLevelSizeSystem, IDisposable
    {
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly ICompanyLevelsSettingProvider _companyLevelsSettingProvider;
        private readonly ILevelProvider _levelProvider;

        private readonly AsyncLazy _readyTask;
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        private float _height;
        private float _width;
        
        public CompanyLevelSizeSystem(
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
            ICompanyLevelsSettingProvider companyLevelsSettingProvider,
            ILevelProvider levelProvider,
            ICameraFormulas cameraFormulas) : base(cameraFormulas)
        {
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _companyLevelsSettingProvider = companyLevelsSettingProvider;
            _levelProvider = levelProvider;

            _readyTask = UniTask.Lazy(WaitReadyAsync);
            
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
        }

        public override async UniTask<float> GetHeightAsync()
        {
            if (_height <= 0)
            {
                var currentLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
                _height = await _companyLevelsSettingProvider.GetLevelHeightAsync(currentLevel);
            }
            
            return _height;
        }

        public override async UniTask<float> GetWidthAsync()
        {
            await _readyTask;

            if (_width <= 0)
            {
                _width = CalculateWidth(_levelProvider.Level.Value.OriginPoint);
            }
            
            return _width;
        }

        private async UniTask WaitReadyAsync()
        {
            try
            {
                await UniTask.WaitWhile(() => _levelProvider.Level.Value == null,
                    cancellationToken: _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e) { }
        }
    }
}