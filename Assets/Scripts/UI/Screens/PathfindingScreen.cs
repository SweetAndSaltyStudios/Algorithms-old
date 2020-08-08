using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class PathfindingScreen : UI_Screen
    {
        #region VARIABLES

        public Transform PathfindingDropdownButton__Content;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        protected override void Awake()
        {
            base.Awake();

            CreatePathfindingDropdownButtons(PathfindingDropdownButton__Content);           
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
