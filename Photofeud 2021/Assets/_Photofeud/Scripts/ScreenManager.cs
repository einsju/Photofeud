using Photofeud.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Photofeud
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance;
        
        [SerializeField] GameObject gameScreen;
        [SerializeField] GameObject loginScreen;

        IList<GameObject> _screens = new List<GameObject>();

        GameObject CurrentScreen => _screens.Last();
        GameObject PreviousScreen => _screens[_screens.IndexOf(CurrentScreen) - 1];

        void Awake() => Instance = this;

        void Start()
        {
            if (!State.Profile.IsAuthenticated)
            {
                OpenScreenWithoutTransition(gameScreen, loginScreen);
                return;
            }

            _screens.Add(gameScreen);
        }

        public void OpenScreenWithoutTransition(GameObject fromScreen, GameObject toScreen)
        {
            fromScreen.SetActive(false);
            toScreen.SetActive(true);
            _screens.Add(toScreen);
        }

        public void OpenGameScreen()
        {
            CanvasHandler.ChangeCanvas(CurrentScreen.transform, gameScreen.transform);
            ResetScreensAndSetFirst(gameScreen);
        }

        public void OpenLoginScreen()
        {
            CanvasHandler.ChangeCanvas(CurrentScreen.transform, loginScreen.transform);
            ResetScreensAndSetFirst(loginScreen);
        }

        void ResetScreensAndSetFirst(GameObject screen)
        {
            _screens.Clear();
            _screens.Add(screen);
        }

        public void OpenScreen(GameObject screen)
        {
            CanvasHandler.ChangeCanvas(CurrentScreen.transform, screen.transform);
            _screens.Add(screen);
        }

        public void CloseScreen()
        {            
            CanvasHandler.ChangeCanvas(CurrentScreen.transform, PreviousScreen.transform);
            _screens.Remove(CurrentScreen);
        }
    }
}
