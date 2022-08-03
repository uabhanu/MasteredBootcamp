using DataSO;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    #region Variables
    
    private float _conquaredDistance;
    private Rigidbody2D _rb2d;
    private Vector2 _startPosition;

    public BulletData BulletData;
    public UnityEvent OnHit = new UnityEvent();

    #endregion

    #region Functions
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _conquaredDistance = Vector2.Distance(transform.position , _startPosition);

        if(_conquaredDistance >= BulletData.MaxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        Debug.Log("Collided : " + col2D.name);
        
        OnHit?.Invoke();

        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(BulletData.Damage);
        }
        
        DisableObject();
    }

    private void DisableObject()
    {
        _rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize(BulletData bulletData)
    {
        BulletData = bulletData;
        _startPosition = transform.position;
        _rb2d.velocity = transform.up * BulletData.Speed;
    }
    
    #endregion
}
