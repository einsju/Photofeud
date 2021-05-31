using Photofeud.Abstractions;
using Photofeud.Authentication;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Settings
{
    public class UpdatePasswordHandler : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_InputField password;
        [SerializeField] TMPro.TMP_InputField repeatPassword;
        [SerializeField] Button updatePassword;

        ProfileUpdateProcessor _processor;
        CanvasGroup _canvasGroup;
        float _alpha;

        void Awake()
        {
            _processor = new ProfileUpdateProcessor(GetComponent<IProfileUpdateService>());
            _canvasGroup = updatePassword.GetComponent<CanvasGroup>();
            _alpha = _canvasGroup.alpha;
        }
        void OnEnable()
        {
            _processor.ProfileUpdated += ProfileUpdated;
            _processor.ProfileUpdateFailed += ProfileUpdateFailed;
        }

        void OnDisable()
        {
            _processor.ProfileUpdated -= ProfileUpdated;
            _processor.ProfileUpdateFailed -= ProfileUpdateFailed;
        }

        public void OnInputValueChanged()
        {
            var hasPassword = !string.IsNullOrEmpty(password.text);
            var hasMatchingPasswords = password.text == repeatPassword.text;

            updatePassword.interactable = hasPassword && hasMatchingPasswords;
            _canvasGroup.alpha = updatePassword.interactable ? 1f : _alpha;
        }

        public void UpdatePassword()
        {
            _processor.UpdatePassword(password.text);
        }

        void ProfileUpdated(object sender, EventArgs e)
        {
            //CloseMenu();
        }

        void ProfileUpdateFailed(object sender, string error)
        {
            //Debug.Log(error);
        }
    }
}
