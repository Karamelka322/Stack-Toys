using CodeBase.Logic.General.Unity.Toys;
using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.Interfaces.General.Systems.Toys
{
    public interface IToyShadowSystem
    {
        UniTask AddAsync(ToyMediator toyMediator);
        void Remove(ToyMediator toyMediator);
    }
}