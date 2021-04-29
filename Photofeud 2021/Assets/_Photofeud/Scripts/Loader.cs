using Photofeud.Abstractions;
using UnityEngine;

namespace Photofeud
{
    public class Loader : MonoBehaviour, ILoader
    {
        [SerializeField] GameObject container;

        public void Load()
        {
            container.SetActive(true);
        }

        public void Stop()
        {
            container.SetActive(false);
        }
    }
}
