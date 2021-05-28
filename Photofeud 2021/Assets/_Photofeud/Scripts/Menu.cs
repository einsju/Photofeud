using Photofeud.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud
{
    [RequireComponent(typeof(Animator))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] RawImage audioButtonMaterial;
        [SerializeField] RawImage vibrationButtonMaterial;
        [SerializeField] GameObject[] controlsThatNeedAuthentication;
        
        Animator _transition;
        bool _hasAudio;
        bool _hasVibration;

        bool IsAuthenticated => State.Profile.PlayerIsSignedIn();
        Color MaterialColor(bool isOn) => isOn ? Color.green : Color.red;

        void Awake()
        {
            _transition = GetComponent<Animator>();
            _hasAudio = Settings.HasAudio;
            _hasVibration = Settings.HasVibration;
            AudioListener.pause = !_hasAudio;
        }

        void Start()
        {
            InitializeButtonMaterialColors();
            ActivateControlsIfAuthenticated();
        }

        void InitializeButtonMaterialColors()
        {
            audioButtonMaterial.color = MaterialColor(_hasAudio);
            vibrationButtonMaterial.color = MaterialColor(!_hasVibration);
        }

        void ActivateControlsIfAuthenticated()
        {
            if (!IsAuthenticated) return;

            foreach (var control in controlsThatNeedAuthentication)
                control.SetActive(true);
        }

        public void OpenMenu()
        {
            _transition.SetTrigger(Triggers.MenuOpen);
        }

        public void CloseMenu()
        {
            _transition.SetTrigger(Triggers.MenuClose);
        }

        public void ToggleAudio()
        {
            _hasAudio = !_hasAudio;
            AudioListener.pause = !_hasAudio;            
            audioButtonMaterial.color = MaterialColor(_hasAudio);
            Settings.SetAudio(_hasAudio);
        }

        public void ToogleVibration()
        {
            _hasVibration = !_hasVibration;            
            vibrationButtonMaterial.color = MaterialColor(!_hasVibration);
            Settings.SetVibration(_hasVibration);
        }

        public void SignOut()
        {

        }
    }
}
