using Random = UnityEngine.Random;
using UnityEngine;

namespace Util
{
    public class ObjectGeneratorAtRandomPosition : MonoBehaviour
    {
        #region Variables
        
        public float Radius = 0.2f;
        public GameObject ObjPrefab;
        
        #endregion

        #region Functions

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position , Radius);
        }

        protected Quaternion Random2DRotation()
        {
            return Quaternion.Euler(0 , 0 , Random.Range(0 , 360));
        }
        
        protected Vector2 GetRandomPosition()
        {
            return Random.insideUnitCircle * Radius + (Vector2)transform.position;
        }

        protected virtual GameObject GetObject()
        {
            return Instantiate(ObjPrefab);
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
