using CodeBase.Logic.Scenes.Infinity.Unity.Toys;

namespace CodeBase.Logic.General.StateMachines.ToyChoicer
{
    public interface IToyChoicerRotateAnimation
    {
        void Play(ToyChoicerMediator toyChoicerMediator);
        void Stop(ToyChoicerMediator toyChoicerMediator);
    }
}