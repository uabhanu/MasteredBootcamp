using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Pool : MonoBehaviour , IPunPrefabPool
    {
        #region Private Variables Declarations

        private Queue<GameObject> _gameObjectPool;

        #endregion
        
        #region Public Variables Declarations

        public GameObject Prefab;

        #endregion

        #region Functions
        
        public void Awake()
        {
            _gameObjectPool = new Queue<GameObject>();
            PhotonNetwork.PrefabPool = this;
        }

        public GameObject Instantiate(string prefabID , Vector3 position , Quaternion rotation)
        {
            if(_gameObjectPool.Count > 0)
            {
                GameObject go = _gameObjectPool.Dequeue();
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);

                return go;
            }
            
            return Instantiate(Prefab , position , rotation);
        }

        public void Destroy(GameObject gameObject)
        {
            gameObject.SetActive(false);
            _gameObjectPool.Enqueue(gameObject);
        }
        
        #endregion
    }
}
