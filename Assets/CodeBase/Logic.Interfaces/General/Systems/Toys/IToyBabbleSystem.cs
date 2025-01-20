using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.General.Systems.Toys
{
    public interface IToyBabbleSystem
    {
        ReactiveDictionary<ToyMediator, GameObject> Babbles { get; }
        
        UniTask AddAsync(ToyMediator toyMediator);
        void Remove(ToyMediator toyMediator);
    }
}