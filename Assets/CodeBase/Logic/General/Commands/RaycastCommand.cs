using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.General.Commands
{
    public class RaycastCommand : IRaycastCommand
    {
        private readonly PointerEventData _pointerEventData;
        private readonly List<RaycastResult> _raycastResults;

        public RaycastCommand()
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
    }
}