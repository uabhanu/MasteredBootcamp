using DataSo;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variable Declarations
    
    private float _travelledDistance;
    private Rigidbody2D _bulletBody2D;
    private Vector2 _startPosition;

    [SerializeField] private BulletDataSo bulletDataSo;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        if(_bulletBody2D == null)
        {
            _bulletBody2D = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        _travelledDistance = Vector2.Distance(transform.position , _startPosition);

        if(_travelledDistance > bulletDataSo.MaxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(bulletDataSo.Damage); 
        }
        
        DisableObject();
    }

    private void DisableObject()
    {
        _bulletBody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize(BulletDataSo bulletData)
    {
        bulletData = bulletDataSo;
        _startPosition = transform.position;
        _bulletBody2D.velocity = transform.up * bulletData.Speed;
    }
    
    #endregion
}
