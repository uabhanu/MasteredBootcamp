using DataSo;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    #region Variables
    
    private float _travelledDistance;
    private Rigidbody2D _bulletBody2D;
    private Vector2 _startPosition;

    [SerializeField] private TurretDataSo turretDataSo;
    [SerializeField] private UnityEvent onHit = new UnityEvent();
    
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

        if(_travelledDistance > turretDataSo.BulletDataSo.MaxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        onHit?.Invoke();
        
        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(turretDataSo.BulletDataSo.Damage); 
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
        bulletData = turretDataSo.BulletDataSo;
        _startPosition = transform.position;
        _bulletBody2D.velocity = transform.up * bulletData.Speed;
    }
    
    #endregion
}
