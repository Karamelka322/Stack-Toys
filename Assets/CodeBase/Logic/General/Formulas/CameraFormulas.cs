using UnityEngine;

namespace CodeBase.Logic.General.Formulas
{
    public class CameraFormulas : ICameraFormulas
    {
        private readonly Camera _camera;

        public CameraFormulas()
        {
            _camera = Camera.main;
        }

        public bool HasLocatedWithinCameraFieldOfView(Vector3 position)
        {
            var viewportPoint = _camera.WorldToViewportPoint(position);
            
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                   viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                   viewportPoint.z > _camera.transform.position.z;
        }
        
        public bool HasLocatedWithinCameraWidth(Vector3 position)
        {
            var viewportPoint = _camera.WorldToViewportPoint(position);
            
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                   viewportPoint.z > _camera.transform.position.z;
        }
    }
}