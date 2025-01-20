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
            var babble = await _babbleFactory.SpawnAsync(toyMediator);
            Babbles.Add(toyMediator, babble);
        }

        public void Remove(ToyMediator toyMediator)
        {
            Object.Destroy(Babbles[toyMediator]);
            Babbles.Remove(toyMediator);
        }
    }
}