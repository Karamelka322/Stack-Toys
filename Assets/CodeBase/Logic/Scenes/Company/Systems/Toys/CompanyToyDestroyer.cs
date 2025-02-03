using System;
using System.Linq;
using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Observers.Toys;
using CodeBase.Logic.Interfaces.General.Providers.Objects.Toys;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Finish;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using Object = UnityEngine.Object;

namespace CodeBase.Logic.Scenes.Company.Systems.Toys
{
    public class CompanyToyDestroyer : IToyDestroyer, IDisposable
    {
        private readonly IToyProvider _toyProvider;
        private readonly CompositeDisposable _compositeDisposable;
        private readonly IFinishObserver _finishObserver;
        private readonly IToyTowerBuildObserver _toyTowerBuildObserver;
        private readonly IToyCountObserver _toyCountObserver;

        public event Action OnDestroyAll;

        public CompanyToyDestroyer(
            IToyTowerBuildObserver toyTowerBuildObserver,
            IToyCountObserver toyCountObserver,
            IFinishObserver finishObserver,
            IToyProvider toyProvider)
        {
            _toyCountObserver = toyCountObserver;
            _toyTowerBuildObserver = toyTowerBuildObserver;
            _finishObserver = finishObserver;
            _toyProvider = toyProvider;
            
            _compositeDisposable = new CompositeDisposable();

            toyTowerBuildObserver.Tower.ObserveAdd().Subscribe(OnAddTowerToy).AddTo(_compositeDisposable);
            _toyTowerBuildObserver.OnTowerFallen += OnTowerFallen;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        private async void OnAddTowerToy(CollectionAddEvent<ToyMediator> addEvent)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            if (_toyCountObserver.NumberOfTowerBuildToys.Value == 0 && _finishObserver.IsFinished.Value == false)
            {
                DestroyAll();
            }
        }

        private void OnTowerFallen()
        {
            DestroyAll();
        }

        private void DestroyAll()
        {
            foreach (var toy in _toyProvider.Toys.ToArray())
            {
                toy.Item2.Reset();
                _toyProvider.Unregister(toy.Item1, toy.Item2);
                Object.Destroy(toy.Item1.gameObject);
            }

            OnDestroyAll?.Invoke();
        }
    }
}