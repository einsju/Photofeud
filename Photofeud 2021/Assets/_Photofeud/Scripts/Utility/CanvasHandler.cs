using UnityEngine;

namespace Photofeud.Utility
{
    public class CanvasHandler : MonoBehaviour
    {
        [SerializeField] float duration = 0.5f;

        public static CanvasHandler Instance;

        Transform _fromCanvas;
        Transform _toCanvas;

        bool _animate;
        float _timeElapsed;
        float _canvasDuration;

        bool CanAnimateFromCanvas => _animate && _fromCanvas.gameObject.activeSelf;
        bool CanAnimateToCanvas => _animate && !_fromCanvas.gameObject.activeSelf;

        public static void ChangeCanvas(Transform fromCanvas, Transform toCanvas)
        {
            Instance.PrepareAnimation(fromCanvas, toCanvas);
            Instance._animate = true;
        }

        void PrepareAnimation(Transform fromCanvas, Transform toCanvas)
        {
            _fromCanvas = fromCanvas;
            _fromCanvas.localScale = Vector3.one;
            _toCanvas = toCanvas;
            _toCanvas.localScale = Vector3.zero;
            _toCanvas.gameObject.SetActive(true);
        }

        void Awake()
        {
            Instance = this;
            _canvasDuration = duration / 2;
        }

        void Update()
        {
            if (CanAnimateFromCanvas) AnimateCanvas(_fromCanvas, Vector3.one, Vector3.zero);
            if (CanAnimateToCanvas) AnimateCanvas(_toCanvas, Vector3.zero, Vector3.one, true);
        }

        void AnimateCanvas(Transform canvas, Vector3 startScale, Vector3 endScale, bool isLast = false)
        {
            if (_timeElapsed < _canvasDuration)
            {
                canvas.localScale = Vector3.Lerp(startScale, endScale, _timeElapsed / _canvasDuration);
                _timeElapsed += Time.deltaTime;
                return;
            }
            
            _timeElapsed = 0f;
            _animate = !isLast;
            canvas.localScale = endScale;
            canvas.gameObject.SetActive(isLast);
        }
    }
}
