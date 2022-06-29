using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Private Variable Declarations
    
    private bool _canSoot = true;
    private Collider2D[] _tankColliders2D;
    private float _currentDelay = 0f;
    
    #endregion

    #region Private Serialized Field Variable Declarations
    
    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> barrelsList;
    
    #endregion

    #region MonoBehaviour Functions
    
    private void Start()
    {
        _tankColliders2D = GetComponentsInParent<Collider2D>();
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
            _canSoot = false;
            _currentDelay = reloadDelay;
        }

        // Not sure why we have to do this way because a turret can only ever have one barrel
        foreach(Transform barrel in barrelsList)
        {
            GameObject bulletObj = Instantiate(bulletPrefab);
            bulletObj.transform.position = barrel.position;
            bulletObj.transform.localRotation = barrel.rotation;
            bulletObj.GetComponent<Bullet>().Initialize();

            foreach(Collider2D collider in _tankColliders2D)
            {
                //Ignore the collision between the tank and bullet created by that tank
                Physics2D.IgnoreCollision(bulletObj.GetComponent<Collider2D>() , collider);
            }
        }
        
        //Debug.Log("Player Pressed Shoot"); // This is not showing in a line per turret but hopefully working fine
    }
    
    #endregion
}
