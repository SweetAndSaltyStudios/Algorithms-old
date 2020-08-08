using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class AlgorithmTabDisplay : MonoBehaviour
    {
        #region VARIABLES

        public Algorithm Algorithm;
        public TextMeshProUGUI TabTitleText;
        public Button TabButton;
        private bool initialized;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void OnValidate()
        {
            if(initialized)
            {
                return;
            }

            if(Algorithm == null || TabTitleText == null)
            {
                initialized = false;
                return;
            }

            TabTitleText.text = Algorithm.Name;

            initialized = true;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
