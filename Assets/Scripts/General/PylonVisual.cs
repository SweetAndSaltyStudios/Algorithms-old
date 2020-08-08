using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class PylonVisual : MonoBehaviour
    {
        #region VARIABLES

        private SpriteRenderer spriteRenderer;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void AnimateColor(Color32 color, float time = 0.1f)
        {
            LeanTween.color(gameObject, color, time).setFromColor(spriteRenderer.color);
        }

        public void AnimatePosition(Vector2 startPosition, Vector2 targetPosition, LeanTweenType tweenType, float time = 0.1f)
        {
            LeanTween.move(gameObject, targetPosition, time).setFrom(startPosition).setEase(tweenType);
        }

        public void AnimateScale(Vector2 startScale, Vector2 targetScale, float time)
        {
            LeanTween.scale(gameObject, targetScale, time).setFrom(startScale);
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
