using System.Collections.Generic;
using UnityEngine;

public class TankTurret : MonoBehaviour
{
    #region Variable Declarations
    
    private bool _bCanShoot;
    private  Collider2D[] _tankColliders2D;

    [SerializeField] private float currentDelay = 0f;
    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> turretBarrelsList;
    
    #endregion

    #region Functions

    private void Awake()
    {
        _tankColliders2D = GetComponentsInParent<Collider2D>();
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
                GameObject bulletObj = Instantiate(bulletPrefab);
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
