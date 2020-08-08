using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class SortingScreen : UI_Screen
    {
        #region VARIABLES

        public Transform SortDropdownButton_Content;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected override void Awake()
        {
            base.Awake();
            CreateSortingDropdownButtons(SortDropdownButton_Content);
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
