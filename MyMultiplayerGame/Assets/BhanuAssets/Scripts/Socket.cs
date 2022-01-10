using System.Collections;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Socket : MonoBehaviour
    {
        #region Private Variables Declarations
        
        private bool _pipeInside;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private BoxCollider socketCollider;
        [SerializeField] private float delay;
        
        #endregion
        
        #region MonoBehaviour Functions

        private IEnumerator ColliderEnable()
        {
            yield return new WaitForSeconds(delay);

            if(!socketCollider.enabled)
            {
                socketCollider.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.tag.Equals("Pipe"))
            {
                socketCollider.enabled = false;
                StartCoroutine(ColliderEnable());
            }
        }

        #endregion
    }
}
