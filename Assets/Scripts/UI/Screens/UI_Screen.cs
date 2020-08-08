using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sweet_And_Salty_Studios
{
    public abstract class UI_Screen : MonoBehaviour
    {
        #region VARIABLES

        [Space]
        [Header("Screen events")]
        public UnityEvent OnScreenOpen = new UnityEvent();
        public UnityEvent OnScreenClose = new UnityEvent();

        private CanvasGroup canvasGroup;

        private Vector2 startingPosition;
        private Vector2 targetPosition;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            targetPosition = transform.position;
            startingPosition = transform.position + Vector3.up * 10;
        }

        protected virtual void Start()
        {
            transform.position = startingPosition;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        protected void CreatePathfindingDropdownButtons(Transform dropdownContent)
        {
            var enumValues = Enum.GetValues(typeof(PATHFINDING_TYPE));

            foreach(var value in enumValues)
            {
                var newButton = Instantiate(UIManager.Instance.UI_ButtonPrefab, dropdownContent);
                newButton.Initialize(
                    value,

                    () =>
                    {
                        PathfindManager.Instance.ChangePathfindingType((PATHFINDING_TYPE)value);
                    }

                    );
            }
        }

        protected void CreateSortingDropdownButtons(Transform dropdownContent)
        {
            var enumValues = Enum.GetValues(typeof(SORTING_TYPE));

            foreach(var value in enumValues)
            {
                var newButton = Instantiate(UIManager.Instance.UI_ButtonPrefab, dropdownContent);
                newButton.Initialize(
                    value,

                    () =>
                    {
                        SortingManager.Instance.ChangeSortingType((SORTING_TYPE)value);
                    }

                    );
            }
        }

        public virtual void Open()
        {
            OnScreenOpen.Invoke();

            LeanTween.move(
                gameObject, 
                targetPosition, 
                1f).setEaseOutBounce()
                .setFrom(startingPosition)
                .setDelay(1f);
        }

        public virtual void Close()
        {
            OnScreenClose.Invoke();

            LeanTween.move(gameObject, startingPosition, 1f).setOnComplete(() => 
            {
                gameObject.SetActive(false);
            });
             
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
