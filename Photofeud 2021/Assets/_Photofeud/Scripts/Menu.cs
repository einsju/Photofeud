using Photofeud.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud
{
    [RequireComponent(typeof(Animator))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] Button newPassword;
        [SerializeField] Button signOut;
        
        Animator _transition;

        void Awake()
        {
            _transition = GetComponent<Animator>();
        }

        void Start()
        {
            var isSignedIn = State.Profile.PlayerIsSignedIn();
            newPassword.gameObject.SetActive(isSignedIn);
            signOut.gameObject.SetActive(isSignedIn);
        }

        public void OpenMenu()
        {
            _transition.SetTrigger(Triggers.MenuOpen);
        }

        public void CloseMenu()
        {
            _transition.SetTrigger(Triggers.MenuClose);
        }
    }
}
