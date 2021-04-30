using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photofeud.Error
{
    public class ErrorHandler : MonoBehaviour, IErrorHandler
    {
        [SerializeField] GameObject container;
        [SerializeField] TMP_Text message;
        [SerializeField] Button close;

        public void HandleError(string message)
        {
            container.SetActive(true);
            this.message.text = message;
        }

        void OnEnable()
        {
            close.onClick.AddListener(Close);
        }

        void OnDisable()
        {
            close.onClick.RemoveListener(Close);
        }

        void Close()
        {
            container.SetActive(false);
        }
    }
}
