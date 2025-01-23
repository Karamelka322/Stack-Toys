using CodeBase.Logic.General.Unity.Toys;
using CodeBase.Logic.Interfaces.General.Factories.Babble;
using CodeBase.Logic.Interfaces.General.Systems.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.General.Systems.Toys
{
    public class ToyBabbleSystem : IToyBabbleSystem
    {
        private readonly IBabbleFactory _babbleFactory;

        public ReactiveDictionary<ToyMediator, GameObject> Babbles { get; }
        
        public ToyBabbleSystem(IBabbleFactory babbleFactory)
        {
            _babbleFactory = babbleFactory;

            Babbles = new ReactiveDictionary<ToyMediator, GameObject>();
        }

        public async UniTask AddAsync(ToyMediator toyMediator)
        {
            if (Babbles.ContainsKey(toyMediator))
            {
                return;
            }
            
            var babble = await _babbleFactory.SpawnAsync(toyMediator);
            Babbles.Add(toyMediator, babble);
        }

        public void Remove(ToyMediator toyMediator)
        {
            if (Babbles.TryGetValue(toyMediator, out var babble))
            {
                Object.Destroy(babble);
                Babbles.Remove(toyMediator);
            }
        }
    }
}