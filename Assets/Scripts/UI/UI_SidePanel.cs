using TMPro;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class UI_SidePanel : UI_Panel
    {
        #region VARIABLES
    
        [SerializeField] TextMeshProUGUI showHideText = null;
        [SerializeField]  private bool isShowing = false;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
    
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void ShowHide()
        {
            isShowing = !isShowing;
            showHideText.text = isShowing ? "HIDE" : "SHOW";
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
