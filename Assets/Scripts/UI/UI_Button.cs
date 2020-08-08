using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace Sweet_And_Salty_Studios
{
    public class UI_Button : UI_Element       
    {
        #region VARIABLES

        public UnityEvent OnClick = new UnityEvent();

        private TextMeshProUGUI buttonText;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS   

        protected override void Awake()
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

            base.Awake();
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

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            OnClick.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            LeanTween.scale(gameObject, defaultScale, 0.05f);
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS       

        public void Initialize(object type, Action onClick)
        {
            var unityAction = new UnityAction(onClick);

            OnClick.AddListener(unityAction);

            buttonText.text = type.ToString();
        }

        #endregion CUSTOM_FUNCTIONS     
    }
}
