using UnityEngine;
using TMPro;

namespace Sweet_And_Salty_Studios
{
    public class TitleScreen : UI_Screen
    {
        #region VARIABLES

        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI FlickeringText;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public override void Open()
        {
            LeanTween.scale(FlickeringText.gameObject,
                Vector2.one * 1.2f, 
                0.6f)
                .setLoopPingPong()
                .setEaseInOutCubic()
                .setDelay(1f);
        }

        public override void Close()
        {
            LeanTween.move(
                FlickeringText.rectTransform, 
                FlickeringText.rectTransform.position + Vector3.down * 100, 
                0.5f);
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
