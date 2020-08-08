using UnityEngine;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class NodeVisual : MonoBehaviour
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

        private void Start()
        {
            spriteRenderer.color = GridManager.Instance.DefaultNodeColor;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void AnimateColor(Color32 color, float time = 0.1f)
        {
            LeanTween.color(gameObject, color, time).setFromColor(spriteRenderer.color);
        }

        public void AnimatePosition(Vector2 startPosition, Vector2 targetPosition,LeanTweenType tweenType, float time = 0.1f)
        {
            LeanTween.move(gameObject, targetPosition, time).setFrom(startPosition).setEase(tweenType);
        }

        public void AnimateScale(Vector2 targetScale, float time)
        {
            LeanTween.scale(gameObject, targetScale, time).setFrom(transform.localScale);         
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
