using System;
using CodeBase.Logic.General.StateMachines;
using CodeBase.Logic.Interfaces.General.Services.SceneLoad;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Cameras;
using CodeBase.Logic.Interfaces.Scenes.Company.Systems.Load;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.States;
using CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine.Transitions;
using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras.StateMachine
{
    public class CameraStateMachine : BaseStateMachine, ICameraStateMachine
    {
        private readonly IDisposable _disposable;
        private readonly Camera _camera;

        private readonly CameraScrollState.Factory _scrollStateFactory;
        private readonly CameraToyFollowState.Factory _toyFollowStateFactory;
        
        private readonly CameraToySelectTransition.Factory _toySelectTransitionFactory;
        private readonly CameraToyUnselectTransition.Factory _toyUnselectTransitionFactory;

        public CameraStateMachine(
            ICompanySceneLoad sceneLoad, 
            CameraScrollState.Factory scrollStateFactory,
            CameraToyFollowState.Factory toyFollowStateFactory,
            CameraToySelectTransition.Factory toySelectTransitionFactory,
            CameraToyUnselectTransition.Factory toyUnselectTransitionFactory)
        {
            _toyUnselectTransitionFactory = toyUnselectTransitionFactory;
            _toyFollowStateFactory = toyFollowStateFactory;
            _toySelectTransitionFactory = toySelectTransitionFactory;
            _camera = Camera.main;
            _scrollStateFactory = scrollStateFactory;
            
            _disposable = sceneLoad.IsLoaded.Subscribe(OnSceneLoaded);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            Reset();
        }

        private void OnSceneLoaded(bool isLoaded)
        {
            if (isLoaded == false)
            {
                return;
            }
            
            Launch();
        }

        protected override StateTree InstallStateTree()
        {
            var scrollState = _scrollStateFactory.Create(_camera);
            var toyFollowState = _toyFollowStateFactory.Create(_camera);
            
            var toySelectTransition = _toySelectTransitionFactory.Create();
            var toyUnselectTransition = _toyUnselectTransitionFactory.Create();
            
            var tree = new StateTree();
            
            tree.RegisterState(scrollState);
            tree.RegisterTransition(scrollState, toySelectTransition, toyFollowState);

            tree.RegisterState(toyFollowState);
            tree.RegisterTransition(toyFollowState, toyUnselectTransition, scrollState);
            
            return tree;
        }
    }
}