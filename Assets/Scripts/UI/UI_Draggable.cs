using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class UI_Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region CUSTOM_FUNCTIONS

        public virtual void OnBeginDrag(PointerEventData eventData)
        {

        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3)eventData.delta;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {

        }

        #endregion CUSTOM_FUNCTIONS
    }
}
