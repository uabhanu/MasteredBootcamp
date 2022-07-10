using UnityEngine;

namespace Util
{
    public class DestroyHelper : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
