using System;
using CodeBase.Logic.Interfaces.Scenes.Company.Observers.Ready;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CodeBase.Logic.Scenes.Company.Systems.Cameras
{
    public class CameraRenderSetup : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly Camera _camera;

        public CameraRenderSetup(ISceneReadyObserver sceneLoad)
        {
            _camera = Camera.main;
            
            _disposable = sceneLoad.IsReady.Subscribe(OnSceneReady);
        }
        
        private void OnSceneReady(bool isReady)
        {
            if (isReady == false)
            {
                return;
            }

            _camera.UpdateVolumeStack();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}