using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class ScreenFader : MonoBehaviour
    {
        #region VARIABLES

        private CanvasGroup canvasGroup;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void Fade(float startingAlpha, float targetAlpha,float fadeDuration)
        {
            canvasGroup.alpha = startingAlpha;
            LeanTween.alphaCanvas(canvasGroup, targetAlpha, fadeDuration).setOnComplete(() =>
            {
                canvasGroup.interactable = canvasGroup.alpha > 0;
                canvasGroup.blocksRaycasts = canvasGroup.alpha > 0;
            });
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
