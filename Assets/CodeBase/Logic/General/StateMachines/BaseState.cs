namespace CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes
{
    /// <summary>
    /// Базовая реализация состояния
    /// </summary>
    public abstract class BaseState
    {
        /// <summary>
        /// При входе в состояние
        /// </summary>
        public virtual void Enter() { }
        
        /// <summary>
        /// При выходе из состояния
        /// </summary>
        public virtual void Exit() { }
    }
}