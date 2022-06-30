using ScriptableObjects;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Private Variable Declarations
    
    private float _travelledDistance = 0f;
    private Vector2 _startPosition;
    
    #endregion
    
    #region Private Serialized Field Variable Declarations
    
    [SerializeField] private BulletData bulletDataSo;
    [SerializeField] private Rigidbody2D bulletBody2D;
    
    #endregion
    
    #region MonoBehaviour Functions
    
    private void Update()
    {
        _travelledDistance = Vector2.Distance(transform.position , _startPosition);

        if(_travelledDistance >= bulletDataSo.MaxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        Damageable damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(bulletDataSo.Damage);
        }
        
        DisableObject();
    }
    
    #endregion

    #region Custom Functions
    
    private void DisableObject()
    {
        bulletBody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize(BulletData bulletData)
    {
        bulletDataSo = bulletData;
        _startPosition = transform.position;
        bulletBody2D.velocity = transform.up * bulletDataSo.Speed;
    }
    
    #endregion
}
