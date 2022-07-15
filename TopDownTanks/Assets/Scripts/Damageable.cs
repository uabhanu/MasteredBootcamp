using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Variables
    
    private int _maxHealth = 100;

    [SerializeField] private int health = 0;
    [SerializeField] private UnityEvent onDead;
    [SerializeField] private UnityEvent<float> onHealthChange;
    [SerializeField] private UnityEvent onHeal;
    [SerializeField] private UnityEvent onHit;
    
    #endregion

    #region Functions

    private void Start()
    {
        if(health == 0)
        {
            Health = _maxHealth;   
        }
    }
    
    public int Health
    {
        get => health;

        set
        {
            health = value;
            onHealthChange?.Invoke((float)Health / _maxHealth);
        }
    }

    public void Heal(int healValue)
    {
        Health += healValue;
        Health = Mathf.Clamp(Health , 0 , _maxHealth);
        onHeal?.Invoke();
    }
    
    public void Hit(int damageValue)
    {
        Health -= damageValue;

        if(Health <= 0)
        {
            onDead?.Invoke();
        }
        else
        {
            onHit?.Invoke();
        }
    }

    #endregion
}
