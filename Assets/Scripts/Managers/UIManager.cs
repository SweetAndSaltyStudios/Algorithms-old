using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class UIManager : Singelton<UIManager>
    {
        #region VARIABLES

        [Space]
        [Header("Screen Events")]
        public UnityEvent OnScreenSwitch = new UnityEvent();

        [Space]
        [Header("Debug Settings")]
        public bool DrawGizmos;
        public Color GridAreaColor = Color.magenta;

        public UI_Button UI_ButtonPrefab;

        public UI_Screen StartingScreen;

        private UI_Screen[] uiScreens;
        private UI_Screen currentScreen;
        private UI_Screen previousScreen;

        private ScreenFader screenFader;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            uiScreens = FindObjectsOfType<UI_Screen>();

            if(screenFader == null)
            {
                screenFader = FindObjectOfType<ScreenFader>();
            }

            if(StartingScreen == null)
            {
                StartingScreen = GameObject.Find("TitleScreen").GetComponent<UI_Screen>();
            }

        }

        private void Start()
        {
            screenFader.Fade(1, 0, 1);

            for(int i = 0; i < uiScreens.Length; i++)
            {
                uiScreens[i].gameObject.SetActive(false);
            }

            SwitchScreen(StartingScreen);
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR

            if(EditorApplication.isPlaying || DrawGizmos == false)
            {
                return;
            }

            Gizmos.color = GridAreaColor;

            Gizmos.DrawWireCube(transform.position, new Vector2(
                GridManager.Instance.GridWorldSize_X,
                GridManager.Instance.GridWorldSize_Y
                ));
#endif
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS    

        public void CreateGridButton()
        {
            GridManager.Instance.CreateGrid();
        }    

        public void FullControlButton()
        {
            SortingManager.Instance.SwitchFullControl();
            PathfindManager.Instance.SwitchFullControl();
        }

        public void CreatePylonsButton()
        {
            SortingManager.Instance.CreatePylons();
        }

        public void StartVisualizingButton()
        {
            SortingManager.Instance.StartSort();
            PathfindManager.Instance.StartSearchAlgorithm();
        }

        public void ClearButton()
        {
            GridManager.Instance.ClearGrid();
            SortingManager.Instance.ClearPylons();
        }

        public void PauseButton()
        {
            PathfindManager.Instance.Pause();
        }

        public void StopButton()
        {
        
        }

        public void QuitButton()
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif            
        }

        public void SwitchScreen(UI_Screen ui_Screen)
        {
            if(currentScreen)
            {
                currentScreen.Close();
                previousScreen = currentScreen;
            }

            currentScreen = ui_Screen;
            currentScreen.gameObject.SetActive(true);
            currentScreen.Open();

            OnScreenSwitch.Invoke();
        }

        public void SwitchPreviousScreen()
        {
            SwitchScreen(previousScreen);
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
