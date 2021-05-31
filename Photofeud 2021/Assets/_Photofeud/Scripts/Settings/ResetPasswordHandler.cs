using Photofeud.Abstractions;
using Photofeud.Authentication;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Settings
{
    public class ResetPasswordHandler : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_InputField email;
        [SerializeField] Button resetPassword;

        ProfileUpdateProcessor _processor;
        CanvasGroup _canvasGroup;
        float _alpha;

        void Awake()
        {
            _processor = new ProfileUpdateProcessor(GetComponent<IProfileUpdateService>());
            _canvasGroup = resetPassword.GetComponent<CanvasGroup>();
            _alpha = _canvasGroup.alpha;
        }
        void OnEnable()
        {
            _processor.ProfileUpdated += ProfileUpdated;
        }

        void OnDisable()
        {
            _processor.ProfileUpdated -= ProfileUpdated;
        }

        public void OnInputValueChanged()
        {
            var hasEmail = !string.IsNullOrEmpty(email.text);

            resetPassword.interactable = hasEmail;
            _canvasGroup.alpha = resetPassword.interactable ? 1f : _alpha;
        }

        public void ResetPassword()
        {
            _processor.ResetPassword(email.text);
        }

        void ProfileUpdated(object sender, EventArgs e)
        {
            //CloseMenu();
        }
    }
}
