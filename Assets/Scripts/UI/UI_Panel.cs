using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class UI_Panel : MonoBehaviour
    {
        #region VARIABLES

        public bool Foo;
        private Vector2 startPosition;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            startPosition = transform.position;

            transform.position += Vector3.up * 10;
        }

        private void Start()
        {
            LeanTween.move(gameObject, startPosition, 1f).setEaseOutBounce();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
