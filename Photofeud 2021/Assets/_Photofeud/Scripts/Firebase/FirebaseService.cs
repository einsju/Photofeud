using Photofeud.Abstractions;
using System.Threading.Tasks;
using UnityEngine;

namespace Photofeud
{
    public class FirebaseService : MonoBehaviour
    {
        protected ITranslator _translator;

        protected bool TaskIsOk(Task task) => !task.IsCanceled && !task.IsFaulted;

        void Awake()
        {
            _translator = GetComponent<ITranslator>();
        }
    }
}
