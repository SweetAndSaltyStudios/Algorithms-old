using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class UI_DropdownButton : UI_Element
    {
        #region VARIABLES

        private Transform dropDownContent = null;
        private CanvasGroup canvasGroup;
        private Vector2 targetScale;
        private Coroutine iHandleCoroutine;
        private WaitForSeconds waitForAnimationDuration;
        private readonly float animationDuration = 0.1f;
        private bool isOpen;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected override void Awake()
        {
            base.Awake();

            dropDownContent = transform.GetChild(1);

            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        private void Start()
        {
            targetScale = transform.localScale;

            waitForAnimationDuration = new WaitForSeconds(animationDuration);

            dropDownContent.localScale *= Vector2.right;

            HandleDropdownContent(isOpen);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            LeanTween.scale(gameObject, activeScale, 0.05f);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            LeanTween.scale(gameObject, defaultScale, 0.05f);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            isOpen = !isOpen;

            HandleDropdownContent(isOpen, animationDuration);
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void HandleDropdownContent(bool isOpen, float animationDuration = 0f)
        {
            if(iHandleCoroutine != null)
            {
                StopCoroutine(iHandleCoroutine);
                return;
            }

            iHandleCoroutine = StartCoroutine(IHandleDropdownContent(isOpen, animationDuration));
        }

        private IEnumerator IHandleDropdownContent(bool isOpen, float animationDuration)
        {          
            if(isOpen == true)
            {
                LeanTween.scaleY(
                    dropDownContent.gameObject,
                    targetScale.y,
                    animationDuration).setFrom(0);
            }
            else
            {
                LeanTween.scaleY(
                    dropDownContent.gameObject,
                    0,
                    animationDuration).setFrom(transform.localScale);
            }

            yield return waitForAnimationDuration;

            canvasGroup.interactable = isOpen;
            canvasGroup.blocksRaycasts = isOpen;

            iHandleCoroutine = null;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
