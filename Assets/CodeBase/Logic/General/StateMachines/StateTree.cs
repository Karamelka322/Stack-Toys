using System;
using System.Collections.Generic;

namespace CodeBase.Logic.General.StateMachines
{
    /// <summary>
    /// Дерево состояний
    /// </summary>
    public class StateTree
    {
        private readonly List<BaseState> _states = new();
        private readonly List<(BaseState, BaseTransition, BaseState)> _transitions = new();

        public IReadOnlyCollection<BaseState> States => _states;
        public IReadOnlyCollection<(BaseState, BaseTransition, BaseState)> Transitions => _transitions;
        
        /// <summary>
        /// Зарегистрировать состояние. Нельзя зарегистрировать два одинаковых состояния
        /// </summary>
        /// <param name="state">Состояние</param>
        public void RegisterState(BaseState state)
        {
            if (_states.Contains(state))
            {
                UnityEngine.Debug.LogError($"State {state.GetType().Name} already registered");
                return;
            }
            
            _states.Add(state);
        }
        
        /// <summary>
        /// Определить переход от одного состояния к другому
        /// </summary>
        /// <param name="transition">Переход</param>
        /// <param name="previewState">То к какому состоянию подвязан переход</param>
        /// <param name="nextState">Если переход отработал, то переходим в это состояние</param>
        public void RegisterTransition(BaseState previewState, BaseTransition transition, BaseState nextState) 
        {
            if (HasTransition(previewState, transition, nextState))
            {
                UnityEngine.Debug.LogError($"Transition {transition.GetType().Name} already registered");
                return;
            }
            
            _transitions.Add((previewState, transition, nextState));
        }

        /// <summary>
        /// Взять первое состояние
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Если дерево состояний пустое</exception>
        public BaseState GetFirstState()
        {
            if (_states.Count == 0)
            {
                throw new ArgumentOutOfRangeException("Not found any state");
            }

            return _states[0];
        }

        /// <summary>
        /// Взять состояние
        /// </summary>
        /// <param name="stateType">Тип состояния</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Если состояние не было зарегистрировано</exception>
        public BaseState GetState(Type stateType)
        {
            foreach (var state in _states)
            {
                if (state.GetType() == stateType)
                {
                    return state;
                }
            }
            
            throw new ArgumentOutOfRangeException($"Not found any state {stateType.Name}");
        }
        
        /// <summary>
        /// Получить все транзишены состояния.
        /// </summary>
        /// <typeparam name="TState">Тип состояния</typeparam>
        /// <returns>(состояние к которому подвязан переход, реализация перехода, следующее состояния)</returns>
        public List<(BaseState, BaseTransition, BaseState)> GetTransitions<TState>(TState state) where TState : BaseState
        {
            var transitions = new List<(BaseState, BaseTransition, BaseState)>();
            
            foreach (var tuple in _transitions)
            {
                if (tuple.Item1 == state)
                {
                    transitions.Add(tuple);
                }
            }

            return transitions;
        }
        
        /// <summary>
        /// Существует ли уже данный переход
        /// </summary>
        /// <param name="transition">Реализация перехода</param>
        /// <param name="previewState">То к какому состоянию подвязан переход</param>
        /// <param name="nextState">Если переход отработал, то переходим в это состояние</param>
        private bool HasTransition(BaseState previewState, BaseTransition transition, BaseState nextState) 
        {
            foreach (var tuple in _transitions)
            {
                if (tuple.Item2 != transition)
                {
                    continue;
                }
                
                if (tuple.Item1 == previewState && tuple.Item3 == nextState)
                {
                    return true;
                }
            }

            return false;
        }
    }
}