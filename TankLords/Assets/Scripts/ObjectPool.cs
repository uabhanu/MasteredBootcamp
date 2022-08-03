using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Variables
    
    protected Queue<GameObject> objectPool; 
        
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;

    public bool AlwaysDestroy;
    public Transform SpawnedObjectsParent;
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }

    private void OnDestroy()
    {
        foreach(var item in objectPool)
        {
            if(item == null)
            {
                continue;
            }
            
            else if(!item.activeSelf || AlwaysDestroy)
            {
                Destroy(item);
            }

            else
            {
                item.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
            }
        }
    }

    private void CreateObjectParentIfNeeded()
    {
        if(SpawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;

            var parentObject = GameObject.Find(name);

            if(parentObject != null)
            {
                SpawnedObjectsParent = parentObject.transform;
            }
            else
            {
                SpawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObject = null;

        if(objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool , transform.position , Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.SetParent(SpawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisabled>();
        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        
        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    public void Initialize(GameObject objectToPool , int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    #endregion
}
