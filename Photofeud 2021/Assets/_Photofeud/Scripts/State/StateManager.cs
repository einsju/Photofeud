using Photofeud.Abstractions;
using Photofeud.Utility;
using System;
using UnityEngine;

namespace Photofeud.State
{
    public class StateManager : MonoBehaviour
    {
        //static event Action<States> StateChangedEventHandler;

        [SerializeField] GameObject[] screens;

        IProfileLoader _profileLoader;
        //States _currentState;

        //States DefaultState => States.Game;

        void Awake()
        {
            //_profileLoader = GetComponent<IProfileLoader>();
            //Profile.SetPlayer(_profileLoader.LoadProfile());

            //_currentState = DefaultState;
            //if (!Profile.IsAuthenticated) ChangeState(States.Login);
            //if (Profile.IsAuthenticated) ChangeState(States.Login);
        }

        void Start()
        {
            // REMOVE THIS CODE
            //Profile.SetPlayer(null);
            //ScreenManager.Instance.OpenLoginScreen();
            //
        }

        //void OnEnable()
        //{
        //    StateChangedEventHandler += ChangeState;
        //}

        //void OnDisable()
        //{
        //    StateChangedEventHandler -= ChangeState;
        //}

        //public static void OnStateChanged(States newState)
        //{
        //    StateChangedEventHandler?.Invoke(newState);
        //}

        //void ChangeState(States newState)
        //{
        //    _currentState = newState;
        //    Debug.Log(_currentState);
        //}
    }
}
