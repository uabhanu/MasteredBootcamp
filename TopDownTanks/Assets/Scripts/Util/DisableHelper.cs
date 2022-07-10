using UnityEngine;

namespace Util
{
    public class DisableHelper : MonoBehaviour
    {
        public void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}
