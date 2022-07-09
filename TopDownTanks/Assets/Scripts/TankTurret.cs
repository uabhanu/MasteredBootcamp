using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class TankTurret : MonoBehaviour
{
    #region Variable Declarations
    
    private bool _bCanShoot;
    private  Collider2D[] _tankColliders2D;
    private int _bulletPoolSize;

    [SerializeField] private float currentDelay = 0f;
    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> turretBarrelsList;
    [SerializeField] private ObjectPool bulletPool;
    
    #endregion

    #region Functions

    private void Awake()
    {
        if(bulletPool == null)
        {
            bulletPool = GetComponent<ObjectPool>();
        }
        
        _bulletPoolSize = bulletPool.PoolSize;

        if(_tankColliders2D == null)
        {
            _tankColliders2D = GetComponentsInParent<Collider2D>();   
        }
    }

    private void Start()
    {
        bulletPool.Initialize(bulletPrefab , _bulletPoolSize);
    }

    private void Update()
    {
        if(!_bCanShoot)
        {
            currentDelay -= Time.deltaTime;

            if(currentDelay <= 0)
            {
                _bCanShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if(_bCanShoot)
        {
            _bCanShoot = false;
            currentDelay = reloadDelay;

            foreach (var barrel in turretBarrelsList)
            {
                //GameObject bulletObj = Instantiate(bulletPrefab);
                GameObject bulletObj = bulletPool.CreateObject();
                bulletObj.transform.position = barrel.position;
                // Barrel Rotation 90 giving wrong result so left it as 0 ignoring the instructor's instruction
                bulletObj.transform.localRotation = barrel.rotation;
                bulletObj.GetComponent<Bullet>().Initialize();

                foreach (var collider in _tankColliders2D)
                {
                    Physics2D.IgnoreCollision(bulletObj.GetComponent<Collider2D>() , collider);
                }
            }
        }
    }
    
    #endregion
}
