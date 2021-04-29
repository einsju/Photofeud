using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photofeud
{
    public class SceneNavigator : MonoBehaviour
    {
        public static void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }

    public class Scene
    {
        public const string Game = "Game";
        public const string Main = "Main";
    }
}
