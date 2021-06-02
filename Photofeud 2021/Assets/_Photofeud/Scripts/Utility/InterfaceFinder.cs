using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photofeud.Utility
{
    public class InterfaceFinder
    {
        static GameObject[] RootGameObjects => SceneManager.GetActiveScene().GetRootGameObjects();

        public static T Find<T>()
        {
            return RootGameObjects.Single(rgo => rgo.GetComponent<T>() != null).GetComponent<T>();
        }
    }
}
