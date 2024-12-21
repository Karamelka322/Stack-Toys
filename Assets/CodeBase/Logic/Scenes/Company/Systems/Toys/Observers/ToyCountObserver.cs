using System;
using CodeBase.Logic.Interfaces.General.Providers.Data.Saves;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Levels;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys.Observers;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys.Observers
{
    public class ToyCountObserver : IToyCountObserver, IDisposable
    {
        private readonly ICompanyLevelsSettingProvider _levelsConfigProvider;
        private readonly ICompanyLevelsSaveDataProvider _companyLevelsSaveDataProvider;
        private readonly IToyTowerObserver _toyTowerObserver;
        private readonly IToyProvider _toyProvider;

        private readonly CompositeDisposable _compositeDisposable;

        public IntReactiveProperty LeftAvailableNumberOfToys { get; }
        public IntReactiveProperty NumberOfOpenToys { get; }
        public IntReactiveProperty MaxNumberOfToys { get; }
        public IntReactiveProperty TowerNumberOfToys { get; }
        public IntReactiveProperty NumberOfTowerBuildToys { get; }

        public ToyCountObserver(
            ICompanyLevelsSaveDataProvider companyLevelsSaveDataProvider,
            ICompanyLevelsSettingProvider levelsConfigProvider,
            IToyTowerObserver toyTowerObserver,
            IToyProvider toyProvider)
        {
            _compositeDisposable = new CompositeDisposable();

            NumberOfTowerBuildToys = new IntReactiveProperty();
            TowerNumberOfToys = new IntReactiveProperty();
            LeftAvailableNumberOfToys = new IntReactiveProperty();
            NumberOfOpenToys = new IntReactiveProperty();
            MaxNumberOfToys = new IntReactiveProperty();
         
            _toyTowerObserver = toyTowerObserver;
            _toyProvider = toyProvider;
            _companyLevelsSaveDataProvider = companyLevelsSaveDataProvider;
            _levelsConfigProvider = levelsConfigProvider;

            _toyTowerObserver.Tower.ObserveAdd().Subscribe(_ => UpdateCounters()).AddTo(_compositeDisposable);
            _toyTowerObserver.Tower.ObserveReset().Subscribe(_ => UpdateCounters()).AddTo(_compositeDisposable);
            
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
            
            TowerNumberOfToys.Value = _toyTowerObserver.Tower.Count;
            NumberOfTowerBuildToys.Value = MaxNumberOfToys.Value - TowerNumberOfToys.Value;
        }
    }
}