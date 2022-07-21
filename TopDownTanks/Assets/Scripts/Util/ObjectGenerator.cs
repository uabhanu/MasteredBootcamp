using UnityEngine;

namespace Util
{
    public class ObjectGenerator : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private float radius = 0.2f;
        [SerializeField] private GameObject objPrefab;
        
        #endregion

        #region Functions

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position , radius);
        }

        protected virtual GameObject GetObject()
        {
            return Instantiate(objPrefab);
        }

        public void CreateObject()
        {
            Vector2 position = transform.position;
            GameObject impactObject = GetObject();
            impactObject.transform.position = position;
        }
        
        #endregion
    }
}
