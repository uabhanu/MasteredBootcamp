using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    #region Private Variable Declarations
    
    private bool _canSoot = true;
    private Collider2D[] _tankColliders2D;
    private float _currentDelay = 0f;
    private int _bulletsPoolCount = 0;
    private ObjectPool _bulletsPool;
    
    #endregion

    #region Private Serialized Field Variable Declarations
    
    [SerializeField] private List<Transform> barrelsList;
    [SerializeField] private TurretData turretDataSo;
    
    #endregion

    #region MonoBehaviour Functions

    private void Awake()
    {
        _bulletsPool = GetComponent<ObjectPool>();
        _bulletsPoolCount = _bulletsPool.PoolSize;
        _tankColliders2D = GetComponentsInParent<Collider2D>();
    }

    private void Start()
    {
        _bulletsPool.Initialize(turretDataSo.BulletPrefab , _bulletsPoolCount);
    }

    private void Update()
    {
        if(!_canSoot)
        {
            _currentDelay -= Time.deltaTime;

            if(_currentDelay <= 0 )
            {
                _canSoot = true;
            }
        }
    }

    #endregion

    #region Custom Functions
    
    public void Shoot()
    {
        if(_canSoot)
        {
            // Not sure why we have to do this way because a turret can only ever have one barrel
            foreach(Transform barrel in barrelsList)
            {
                //GameObject bulletObj = Instantiate(bulletPrefab); // Regular method which involves Garbage Collector
                GameObject bulletObj = _bulletsPool.CreateObject(); // Optimized method using Object Pool Design
                bulletObj.transform.position = barrel.position;
                bulletObj.transform.localRotation = barrel.rotation;
                bulletObj.GetComponent<Bullet>().Initialize(turretDataSo.BulletDataSo);

                foreach(Collider2D collider in _tankColliders2D)
                {
                    //Ignore the collision between the tank and bullet created by that tank
                    Physics2D.IgnoreCollision(bulletObj.GetComponent<Collider2D>() , collider);
                }
            }
            
            _canSoot = false;
            _currentDelay = turretDataSo.ReloadDelay; //TODO This doesn't seem to be used at all so rewatch the shooting video
        }

        //Debug.Log("Player Pressed Shoot"); // This is not showing in a line per turret but hopefully working fine
    }
    
    #endregion
}
