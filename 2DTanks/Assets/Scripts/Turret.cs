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
            _currentDelay = Time.deltaTime;

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
        Debug.Log("Player Pressed Shoot"); //This is not showing in a line per turret but hopefully working fine
    }
    
    #endregion
}
