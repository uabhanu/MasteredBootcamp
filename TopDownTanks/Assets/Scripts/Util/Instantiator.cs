using UnityEngine;

namespace Util
{
    public class Instantiator : MonoBehaviour
    {
        public GameObject ObjToInstantiate;

        public void InstantiateObject()
        {
            Instantiate(ObjToInstantiate);
        }
    }
}
