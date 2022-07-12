using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class ObjectPool : MonoBehaviour
    {
        #region Variables
    
        //Not Sure why protected instead of private
        protected Queue<GameObject> _obJectPoolQueue;
    
        [SerializeField] protected GameObject objectToPool;
        [SerializeField] protected int poolSize = 5;
        [SerializeField] private Transform spawnedObjectsParentTransform;

        public bool B_AlwaysDestroy = false;
        public int PoolSize => poolSize;

        #endregion

        #region Functions
    
        private void Awake()
        {
            _obJectPoolQueue = new Queue<GameObject>();
        }

        private void OnDestroy()
        {
            foreach(var obj in _obJectPoolQueue)
            {
                if(obj == null)
                {
                    continue;
                }

                else if(!obj.activeSelf || B_AlwaysDestroy)
                {
                    Destroy(obj);
                }
            
                else
                {
                    obj.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
                }
            }
        }

        private void CreateObjectParentIfNeeded()
        {
            if(spawnedObjectsParentTransform == null)
            {
                string name = "ObjectPool_" + objectToPool.name;
                var parentObj = GameObject.Find(name);

                // This check is useful because both player and enemy could have created a pool and if so, simply reuse it
                if(parentObj != null)
                {
                    spawnedObjectsParentTransform = parentObj.transform;
                }
                else
                {
                    spawnedObjectsParentTransform = new GameObject(name).transform;
                }
            }
        }

        public GameObject CreateObject()
        {
            CreateObjectParentIfNeeded();
            GameObject spawnedObject = null;

            if(_obJectPoolQueue.Count < poolSize)
            {
                spawnedObject = Instantiate(objectToPool , transform.position , Quaternion.identity);
                spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + _obJectPoolQueue.Count;
                spawnedObject.transform.SetParent(spawnedObjectsParentTransform);
                spawnedObject.AddComponent<DestroyIfDisabled>();
            }
            else
            {
                spawnedObject = _obJectPoolQueue.Dequeue();
                spawnedObject.transform.position = transform.position;
                spawnedObject.transform.rotation = Quaternion.identity; 
                spawnedObject.SetActive(true);
            }
        
            _obJectPoolQueue.Enqueue(spawnedObject);
            return spawnedObject;
        }

        public void Initialize(GameObject objToPool , int pSize)
        {
            objectToPool = objToPool;
            poolSize = pSize;
        }
    
        #endregion
    }
}
