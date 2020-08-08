using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public abstract class UI_Element : MonoBehaviour,
        IPointerEnterHandler,
        IPointerDownHandler,
        IPointerClickHandler,
        IPointerExitHandler,
        IPointerUpHandler
    {
        #region VARIABLES

        protected Vector2 defaultScale;
        protected Vector2 activeScale;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected virtual void Awake()
        {
            defaultScale = transform.localScale;
            activeScale = defaultScale * 1.1f;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if(InputManager.Instance.DebugPointerEvents)
            {
                Debug.Log("OnPointerEnter", gameObject);
            }   
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if(InputManager.Instance.DebugPointerEvents)
            {
                Debug.Log("OnPointerDown", gameObject);
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if(InputManager.Instance.DebugPointerEvents)
            {
                Debug.Log("OnPointerClick", gameObject);
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if(InputManager.Instance.DebugPointerEvents)
            {
                Debug.Log("OnPointerExit", gameObject);
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if(InputManager.Instance.DebugPointerEvents)
            {
                Debug.Log("OnPointerUp", gameObject);
            }
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
