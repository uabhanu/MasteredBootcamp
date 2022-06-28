using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Private Variable Declarations
    
    private float _travelledDistance = 0f;
    private Vector2 _startPosition;
    
    #endregion
    
    #region Private Serialized Field Variable Declarations
    
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D bulletBody2D;
    
    #endregion
    
    #region MonoBehaviour Functions
    
    private void Update()
    {
        _travelledDistance = Vector2.Distance(transform.position , _startPosition);

        if(_travelledDistance >= maxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        Damageable damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(damage);
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

    public void Initialize()
    {
        _startPosition = transform.position;
        bulletBody2D.velocity = transform.up * speed;
    }
    
    #endregion
}
