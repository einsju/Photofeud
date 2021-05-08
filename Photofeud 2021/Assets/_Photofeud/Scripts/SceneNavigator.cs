using Photofeud.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photofeud
{
    public class SceneNavigator : MonoBehaviour
    {
        [SerializeField] Animator transition;
        [SerializeField] float transitionTime = 0.5f;

        public static SceneNavigator Instance;

        void Awake()
        {
            Instance = this;
        }

        public static void LoadScene(string scene)
        {
            Instance.StartCoroutine(Instance.LoadSceneAfterDelay(scene));
        }

        IEnumerator LoadSceneAfterDelay(string scene)
        {
            transition.SetTrigger(Triggers.FadeOut);
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(scene);
        }
    }
}
