using UnityEngine;

namespace Util
{
    public class Instantiator : MonoBehaviour
    {
        [SerializeField] private GameObject objToInstantiate;

        public void InstantiateObject()
        {
            Instantiate(objToInstantiate);
        }
    }
}
