using UniRx;

namespace CodeBase.CodeBase.Logic.Scenes.World.Systems.Heroes
{
    /// <summary>
    /// Базовая реализация логики перехода между состояниями
    /// </summary>
    public abstract class BaseTransition
    {
        /// <summary>
        /// Когда происходит вызов этого события, срабатывает переход
        /// </summary>
        public readonly BoolReactiveProperty IsCompleted = new();
        
        /// <summary>
        /// Отрабатывает при запуске состояния к которому прикреплен данный транзишен
        /// </summary>
        public virtual void Enter() { }
        
        /// <summary>
        /// Отрабатывает при выходе из состояния к которому прикреплен данный транзишен
        /// </summary>
        public virtual void Exit() { }
    }
}