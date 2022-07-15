using Random = UnityEngine.Random;
using UnityEngine;

namespace Util
{
    public class ObjectGeneratorAtRandomPosition : MonoBehaviour
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

        protected Quaternion Random2DRotation()
        {
            return Quaternion.Euler(0 , 0 , Random.Range(0 , 360));
        }
        
        protected Vector2 GetRandomPosition()
        {
            return Random.insideUnitCircle * radius + (Vector2)transform.position;
        }

        protected virtual GameObject GetObject()
        {
            return Instantiate(objPrefab);
        }

        public void CreateObject()
        {
            Vector2 position = GetRandomPosition();
            GameObject impactObject = GetObject();
            impactObject.transform.position = position;
            impactObject.transform.rotation = Random2DRotation();
        }
        
        #endregion
    }
}
