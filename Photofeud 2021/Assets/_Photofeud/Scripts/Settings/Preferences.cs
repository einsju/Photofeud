using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Settings
{
    public class Preferences : MonoBehaviour
    {
        [SerializeField] RawImage audioButtonMaterial;
        [SerializeField] RawImage vibrationButtonMaterial;

        bool _hasAudio;
        bool _hasVibration;

        Color MaterialColor(bool isOn) => isOn ? Color.green : Color.red;

        void Awake()
        {
            _hasAudio = Utility.Settings.HasAudio;
            _hasVibration = Utility.Settings.HasVibration;
            AudioListener.pause = !_hasAudio;
        }

        void Start()
        {
            InitializeButtonMaterialColors();
        }

        void InitializeButtonMaterialColors()
        {
            audioButtonMaterial.color = MaterialColor(_hasAudio);
            vibrationButtonMaterial.color = MaterialColor(!_hasVibration);
        }

        public void ToggleAudio()
        {
            _hasAudio = !_hasAudio;
            AudioListener.pause = !_hasAudio;
            audioButtonMaterial.color = MaterialColor(_hasAudio);
            Utility.Settings.SetAudio(_hasAudio);
        }

        public void ToogleVibration()
        {
            _hasVibration = !_hasVibration;
            vibrationButtonMaterial.color = MaterialColor(!_hasVibration);
            Utility.Settings.SetVibration(_hasVibration);
        }
    }
}
