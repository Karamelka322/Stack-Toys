using System.Collections.Generic;
using CodeBase.Logic.Interfaces.General.Formulas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.General.Formulas
{
    public class ClickFormulas : IClickFormulas
    {
        private readonly PointerEventData _pointerEventData;
        private readonly List<RaycastResult> _raycastResults;

        public ClickFormulas()
        {
            _pointerEventData = new PointerEventData(EventSystem.current);
            _raycastResults = new List<RaycastResult>();
        }
        
        public bool HasSelect(Vector3 clickPosition, GameObject target)
        {
            _pointerEventData.position = clickPosition;
            
            EventSystem.current.RaycastAll(_pointerEventData, _raycastResults);

            foreach (var raycastResult in _raycastResults)
            {
                if (raycastResult.gameObject == target)
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool HasUI(Vector3 clickPosition)
        {
            _pointerEventData.position = clickPosition;
            
            EventSystem.current.RaycastAll(_pointerEventData, _raycastResults);

            return _raycastResults.Count > 0;
        }
        
        public Vector3 ClickToWorldPosition(Camera camera, Vector3 clickPosition, Transform target)
        {
            var ray = camera.ScreenPointToRay(clickPosition);
            
            var distance = Vector3.Distance(ray.origin, target.position);
            var worldPositiom = ray.origin + ray.direction * distance;
            worldPositiom.z = target.position.z;
            
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
            
            return worldPositiom;
        }
    }
}