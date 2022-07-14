using DataSo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Util;

[RequireComponent(typeof(ObjectPool))]
public class TankTurret : MonoBehaviour
{
    #region Variables
    
    private bool _bCanShoot;
    private  Collider2D[] _tankColliders2D;
    private int _bulletPoolSize;

    [SerializeField] private float currentDelay = 0f;
    [SerializeField] private List<Transform> turretBarrelsList;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private TurretDataSo turretDataSo;

    public UnityEvent OnCantShoot;
    public UnityEvent OnShoot;
    public UnityEvent<float> OnReloading;

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
        bulletPool.Initialize(turretDataSo.BulletPrefab , _bulletPoolSize);
        OnReloading?.Invoke(currentDelay);
    }

    private void Update()
    {
        if(!_bCanShoot)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay / turretDataSo.ReloadDelay);

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
            currentDelay = turretDataSo.ReloadDelay;

            foreach (var barrel in turretBarrelsList)
            {
                //GameObject bulletObj = Instantiate(bulletPrefab);
                GameObject bulletObj = bulletPool.CreateObject();
                bulletObj.transform.position = barrel.position;
                // Barrel Rotation 90 giving wrong result so left it as 0 ignoring the instructor's instruction
                bulletObj.transform.localRotation = barrel.rotation;
                bulletObj.GetComponent<Bullet>().Initialize(turretDataSo.BulletDataSo);

                foreach (var collider in _tankColliders2D)
                {
                    Physics2D.IgnoreCollision(bulletObj.GetComponent<Collider2D>() , collider);
                }
            }
            
            OnShoot?.Invoke();
            OnReloading?.Invoke(currentDelay);
            
        }
        else
        {
            OnCantShoot?.Invoke();
        }
    }
    
    #endregion
}
