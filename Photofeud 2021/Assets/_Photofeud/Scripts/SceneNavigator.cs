using Photofeud.Loading;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photofeud
{
    public class SceneNavigator : MonoBehaviour
    {
        public static SceneNavigator Instance;

        ILoader _loader;

        void Awake()
        {
            Instance = this;
            _loader = FindObjectsOfType<MonoBehaviour>().OfType<ILoader>().FirstOrDefault();
        }

        public static void LoadScene(string scene)
        {
            Instance.StartCoroutine(Instance.LoadSceneAfterDelay(scene));
        }

        public static void GoBack(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        IEnumerator LoadSceneAfterDelay(string scene)
        {
            _loader.Load();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(scene);
        }
    }

    public class Scene
    {
        public const string Game = "Game";
        public const string Main = "Main";
        public const string Leaderboard = "Leaderboard";
    }
}
