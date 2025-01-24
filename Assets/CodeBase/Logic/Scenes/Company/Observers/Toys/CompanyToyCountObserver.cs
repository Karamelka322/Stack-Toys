using System;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Observers.Toys
{
    public class CompanyToyCountObserver : IToyCountObserver, IDisposable
    {
        private readonly ICompanyLevelsSettingProvider _levelsConfigProvider;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly IToyTowerBuildObserver _toyTowerBuildObserver;
        private readonly IToyProvider _toyProvider;

        private readonly CompositeDisposable _compositeDisposable;

        public IntReactiveProperty LeftAvailableNumberOfToys { get; }
        public IntReactiveProperty NumberOfOpenToys { get; }
        public IntReactiveProperty MaxNumberOfToys { get; }
        public IntReactiveProperty TowerNumberOfToys { get; }
        public IntReactiveProperty NumberOfTowerBuildToys { get; }

        public CompanyToyCountObserver(
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
            ICompanyLevelsSettingProvider levelsConfigProvider,
            IToyTowerBuildObserver toyTowerBuildObserver,
            IToyProvider toyProvider)
        {
            _compositeDisposable = new CompositeDisposable();

            NumberOfTowerBuildToys = new IntReactiveProperty();
            TowerNumberOfToys = new IntReactiveProperty();
            LeftAvailableNumberOfToys = new IntReactiveProperty();
            NumberOfOpenToys = new IntReactiveProperty();
            MaxNumberOfToys = new IntReactiveProperty();
         
            _toyTowerBuildObserver = toyTowerBuildObserver;
            _toyProvider = toyProvider;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _levelsConfigProvider = levelsConfigProvider;

            _toyTowerBuildObserver.Tower.ObserveAdd().Subscribe(_ => UpdateCounters()).AddTo(_compositeDisposable);
            _toyTowerBuildObserver.Tower.ObserveReset().Subscribe(_ => UpdateCounters()).AddTo(_compositeDisposable);
            
            _toyProvider.Toys.ObserveAdd().Subscribe(_ => UpdateCounters()).AddTo(_compositeDisposable);
            _toyProvider.Toys.ObserveRemove().Subscribe(_ => UpdateCounters()).AddTo(_compositeDisposable);

            InitializeAsync().Forget();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            
            LeftAvailableNumberOfToys?.Dispose();
            NumberOfOpenToys?.Dispose();
            MaxNumberOfToys?.Dispose();
        }

        private async UniTask InitializeAsync()
        {
            var currentLevel = _companyLevelsSaveDataProvider.GetCurrentLevel();
            var toyPrefabs = await _levelsConfigProvider.GetToyPrefabsAsync(currentLevel);

            MaxNumberOfToys.Value = toyPrefabs.Length;
        
            UpdateCounters();
        }

        private void UpdateCounters()
        {
            NumberOfOpenToys.Value = _toyProvider.Toys.Count;
            LeftAvailableNumberOfToys.Value = MaxNumberOfToys.Value - NumberOfOpenToys.Value;
            
            TowerNumberOfToys.Value = _toyTowerBuildObserver.Tower.Count;
            NumberOfTowerBuildToys.Value = MaxNumberOfToys.Value - TowerNumberOfToys.Value;
        }
    }
}