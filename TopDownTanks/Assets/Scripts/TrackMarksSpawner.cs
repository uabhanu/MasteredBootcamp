using UnityEngine;
using Util;

public class TrackMarksSpawner : MonoBehaviour
{
    #region Variables
    
    private ObjectPool _objectPool;
    private Vector2 _lastPosition;

    [SerializeField] private float trackDistance = 0.2f;
    [SerializeField] private GameObject trackMarksPrefab;
    [SerializeField] private int objectPoolSize = 50;
    
    #endregion
    
    #region Functions
    
    private void Awake()
    {
        _objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _lastPosition = transform.position;
        _objectPool.Initialize(trackMarksPrefab , objectPoolSize);
    }

    private void Update()
    {
        var distanceDriven = Vector2.Distance(transform.position , _lastPosition);

        if(distanceDriven >= trackDistance)
        {
            _lastPosition = transform.position;

            var tracks = _objectPool.CreateObject();
            tracks.transform.position = transform.position;
            tracks.transform.rotation = transform.rotation;
        }
    }
    
    #endregion
}
