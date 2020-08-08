using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Pylon
    {
        #region VARIABLES

        #endregion VARIABLES

        #region PROPERTIES

        public PylonVisual PylonVisual
        {
            get;
            set;
        }

        public int Value
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region CONSTRUCTORS

        public Pylon(int value, PylonVisual pylonVisual)
        {
            Value = value;
            PylonVisual = pylonVisual;
        }

        #endregion CONSTRUCTORS

        #region UNITY_FUNCTIONS

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        #endregion CUSTOM_FUNCTIONS
    }
}
