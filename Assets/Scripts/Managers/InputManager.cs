using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class InputManager : Singelton<InputManager>
    {
        #region VARIABLES

        public bool DebugPointerEvents = false;

        private Camera mainCamera;

        #endregion VARIABLES

        #region PROPERTIES

        public bool IsOverUI
        {
            get
            {
                return EventSystem.current.IsPointerOverGameObject();
            }
        }

        public Vector2 MouseWorldPosition
        {
            get
            {
                return mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public bool IsMouseLeftDown
        {
            get
            {
                return Input.GetMouseButtonDown(0);
            }
        }

        public bool IsMouseLeftPressed
        {
            get
            {
                return Input.GetMouseButton(0);
            }
        }

        public bool IsMouseLeftUp
        {
            get
            {
                return Input.GetMouseButtonUp(0);
            }
        }

        public bool IsMouseRightDown
        {
            get
            {
                return Input.GetMouseButtonDown(1);
            }
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public Vector2 GetWorldPointFromScreenPoint(Vector2 screenPoint)
        {
            return mainCamera.ScreenToWorldPoint(screenPoint);
        }

        #endregion CUSTOM_FUNCTIONS
        }
    }
