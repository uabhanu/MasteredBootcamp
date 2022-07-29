using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Variables

    private bool _canShoot = true;
    private Collider2D[] _tankColliders;

    public float CurrentDelay = 0f;
    public float ReloadDelay = 1f;
    public GameObject BulletPrefab;
    public List<Transform> TurretBarrels;
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        _tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Update()
    {
        if(!_canShoot)
        {
            CurrentDelay -= Time.deltaTime;

            if(CurrentDelay <= 0)
            {
                _canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if(_canShoot)
        {
            _canShoot = false;
            CurrentDelay = ReloadDelay;

            foreach(var barrel in TurretBarrels)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize();

                foreach(var collider in _tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>() , collider);
                }
            }
        }
    }
    
    #endregion
}
