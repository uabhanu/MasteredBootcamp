using UnityEngine;

namespace Utils
{
    public class DestroyHelper : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
