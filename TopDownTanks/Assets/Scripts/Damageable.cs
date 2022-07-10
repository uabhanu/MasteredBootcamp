using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private int _maxHealth = 100; // Remove SerializedField after testing

    [SerializeField] private int health;

    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHeal;
    public UnityEvent OnHit;
    
    #endregion

    #region Functions

    private void Start()
    {
        Health = _maxHealth;
    }
    
    public int Health
    {
        get => health;

        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / _maxHealth);
        }
    }

    public void Heal(int healValue)
    {
        Health += healValue;
        Health = Mathf.Clamp(Health , 0 , _maxHealth);
        OnHeal?.Invoke();
    }
    
    public void Hit(int damageValue)
    {
        Health -= damageValue;

        if(Health <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    #endregion
}
