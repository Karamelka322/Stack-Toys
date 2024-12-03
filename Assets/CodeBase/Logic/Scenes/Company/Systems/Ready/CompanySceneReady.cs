using System;
using CodeBase.Logic.General.Unity.Finish;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Systems.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using CodeBase.Logic.Scenes.Company.Systems.Toys.StateMachine;
using CodeBase.UI.Interfaces.Scenes.Company.Windows.Main;
using UniRx;

namespace CodeBase.Logic.Scenes.Company.Systems.Ready
{
    public class CompanySceneReady : ICompanySceneReady, IDisposable
    {
        private readonly IFinishLineSpawner _finishLineSpawner;
        private readonly ILevelSpawner _levelSpawner;
        private readonly IToySpawner _toySpawner;
        private readonly CompositeDisposable _compositeDisposable;

        private bool _isLevelSpawned;
        private bool _isFirstToySpawned;
        private bool _isFinishLineSpawned;
        private bool _isMainWindowOpened;
        
        public BoolReactiveProperty IsReady { get; }
        
        public CompanySceneReady(
            ILevelSpawner levelSpawner,
            IToySpawner toySpawner,
            IFinishLineSpawner finishLineSpawner,
            ICompanyMainWindow companyMainWindow)
        {
            _finishLineSpawner = finishLineSpawner;
            _toySpawner = toySpawner;

            _compositeDisposable = new CompositeDisposable();
            IsReady = new BoolReactiveProperty();

            _finishLineSpawner.OnSpawn += OnFinishLineSpawn;
            _toySpawner.OnSpawn += OnToySpawn;

            companyMainWindow.IsOpened.Subscribe(OnMainWindowOpening).AddTo(_compositeDisposable);
            levelSpawner.IsLoaded.Subscribe(OnLevelSpawn).AddTo(_compositeDisposable);
        }
        
        public void Dispose()
        {
            _finishLineSpawner.OnSpawn -= OnFinishLineSpawn;
            _toySpawner.OnSpawn -= OnToySpawn;
            
            _compositeDisposable?.Dispose();
            IsReady?.Dispose();
        }
        
        private void OnLevelSpawn(bool isLoaded)
        {
            if (_isLevelSpawned)
            {
                return;
            }
            
            _isLevelSpawned = isLoaded;
            UpdateReady();
        }
        
        private void OnToySpawn(ToyMediator toyMediator, ToyStateMachine toyStateMachine)
        {
            if (_isFirstToySpawned)
            {
                return;
            }
            
            _isFirstToySpawned = toyMediator != null;
            UpdateReady();
        }
        
        private void OnFinishLineSpawn(FinishLineMediator finishLineMediator)
        {
            if (_isFinishLineSpawned)
            {
                return;
            }
            
            _isFinishLineSpawned = finishLineMediator != null;
            UpdateReady();
        }
        
        private void OnMainWindowOpening(bool isOpened)
        {
            if (_isMainWindowOpened)
            {
                return;
            }

            _isMainWindowOpened = isOpened;
            UpdateReady();
        }
        
        private void UpdateReady()
        {
            IsReady.Value = _isLevelSpawned && _isFirstToySpawned && _isFinishLineSpawned && _isMainWindowOpened;
        }
    }
}