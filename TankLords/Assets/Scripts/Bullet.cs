using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    
    private float _conquaredDistance;
    private Rigidbody2D _rb2d;
    private Vector2 _startPosition;
    
    public float MaxDistance = 15f;
    public float Speed = 10f;
    public int Damage = 10;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _conquaredDistance = Vector2.Distance(transform.position , _startPosition);

        if(_conquaredDistance >= MaxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        Debug.Log("Collided : " + col2D.name);

        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(Damage);
        }
        
        DisableObject();
    }

    private void DisableObject()
    {
        _rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        _startPosition = transform.position;
        _rb2d.velocity = transform.up * Speed;
    }
    
    #endregion
}
