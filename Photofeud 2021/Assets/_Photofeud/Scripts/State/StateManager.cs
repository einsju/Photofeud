using System;
using UnityEngine;

namespace Photofeud.State
{
    public class StateManager : MonoBehaviour
    {
        public static event Action PlayerLoggedInEventHandler;
        public static event Action PlayerLoggedOutEventHandler;

        public static void OnPlayerLoggedIn()
        {
            PlayerLoggedInEventHandler?.Invoke();
        }

        public static void OnPlayerLoggedOut()
        {
            Profile.SetPlayer(null);
            PlayerLoggedOutEventHandler?.Invoke();
        }
    }
}
