using DataSo;
using UnityEngine;
using Util;

public class TrackMarksSpawner : MonoBehaviour
{
    #region Variables
    
    private ObjectPool _objectPool;
    private Vector2 _lastPosition;

    [SerializeField] private TrackMarksSo trackMarksSo;
    
    #endregion
    
    #region Functions
    
    private void Awake()
    {
        _objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _lastPosition = transform.position;
        _objectPool.Initialize(trackMarksSo.TrackMarksPrefab , trackMarksSo.ObjectPoolSize);
    }

    private void Update()
    {
        var distanceDriven = Vector2.Distance(transform.position , _lastPosition);

        if(distanceDriven >= trackMarksSo.TrackDistance)
        {
            _lastPosition = transform.position;

            var tracks = _objectPool.CreateObject();
            tracks.transform.position = transform.position;
            tracks.transform.rotation = transform.rotation;
        }
    }
    
    #endregion
}
