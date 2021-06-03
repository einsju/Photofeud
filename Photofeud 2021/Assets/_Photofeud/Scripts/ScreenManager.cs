using Photofeud.State;
using UnityEngine;

namespace Photofeud
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance;

        [SerializeField] Screen gameScreen;
        [SerializeField] Screen loginScreen;

        IScreenTransition _transition;
        ScreenStack _stack;

        void Awake()
        {
            Instance = this;
            _transition = GetComponent<IScreenTransition>();
            _stack = new ScreenStack(gameScreen);
        }
        void OnEnable()
        {
            StateManager.PlayerLoggedInEventHandler += PlayerLoggedIn;
            StateManager.PlayerLoggedOutEventHandler += PlayerLoggedOut;
        }

        void OnDisable()
        {
            StateManager.PlayerLoggedInEventHandler -= PlayerLoggedIn;
            StateManager.PlayerLoggedOutEventHandler -= PlayerLoggedOut;
        }

        void PlayerLoggedIn()
        {
            _transition.ExecuteTransition(_stack.CurrentScreen, gameScreen);
            _stack.ClearHistoryAndSetTop(gameScreen);
        }

        void PlayerLoggedOut()
        {
            _transition.ExecuteTransition(_stack.CurrentScreen, loginScreen);
            _stack.ClearHistoryAndSetTop(loginScreen);
        }

        void Start()
        {
            if (!State.Profile.IsAuthenticated)
            {
                OpenScreenWithoutTransition(gameScreen, loginScreen);
                return;
            }

            _stack.Add(gameScreen);
        }

        void OpenScreenWithoutTransition(Screen fromScreen, Screen toScreen)
        {
            fromScreen.gameObject.SetActive(false);
            toScreen.gameObject.SetActive(true);
            _stack.Add(toScreen);
        }

        public void OpenScreen(Screen screen)
        {
            _transition.ExecuteTransition(_stack.CurrentScreen, screen);
            _stack.Add(screen);
        }

        public void CloseScreen()
        {
            _transition.ExecuteTransition(_stack.CurrentScreen, _stack.PreviousScreen);
            _stack.Remove(_stack.CurrentScreen);
        }
    }
}
