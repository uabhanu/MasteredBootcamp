using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObjectPool : MonoBehaviour
{
    #region Protected Variable Declerations
    
    //First in First out Data Structure
    protected GameObject _objectToPool;
    protected Queue<GameObject> _objectPool;
    
    #endregion
    
    #region Serialized Field Protected Variable Declerations
    
    [SerializeField] protected int poolSize = 10;
    
    #endregion

    #region Public Variable Declerations
    
    public Transform spawnedObjectsParent;
    
    #endregion

    #region MonoBehaviour Functions
    
    private void Awake()
    {
        _objectPool = new Queue<GameObject>();
    }

    private void OnDestroy()
    {
        foreach(GameObject gobj in _objectPool)
        {
            if(gobj != null)
            {
                if(!gobj.activeSelf)
                {
                    Destroy(gobj);
                }
                else
                {
                    gobj.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
                } 
            }
        }
    }

    #endregion
    
    #region Custom Functions

    private void CreateObjectParentIfNeeded()
    {
        if(spawnedObjectsParent == null)
        {
            string name = "Object Pool : " + _objectToPool.name;
            
            GameObject parentGObj = GameObject.Find(name);

            if(parentGObj != null)
            {
                spawnedObjectsParent = parentGObj.transform;
            }
            else
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();
        
        GameObject spawnedObject = null;

        if(_objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(_objectToPool , transform.position , Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + _objectToPool.name + "_" + _objectPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisabled>(); // To avoid adding to all the objects in question manually and avoid danger of forgetting it
        }
        else
        {
            spawnedObject = _objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        
        _objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    public void Initialize(GameObject objToPool , int pSize = 10)
    {
        _objectToPool = objToPool;
        poolSize = pSize;
    }
    
    #endregion
    
    #region Getters & Setters
    
    public int PoolSize
    {
        get => poolSize;
        set => poolSize = value;
    }
    
    #endregion
}
