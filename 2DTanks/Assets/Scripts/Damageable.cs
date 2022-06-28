using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Serialized Field Private Variable Declerations
    
    [SerializeField] private int health;
    
    #endregion
    
    #region Public Variable Declerations 
    
    public int MaxHealth = 100;
    public UnityEvent OnDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnHeal;
    public UnityEvent OnHit;
    
    #endregion
    
    #region MonoBehaviour Functions
    
    private void Start()
    {
        Health = MaxHealth;
    }
    
    #endregion

    #region Custom Functions
    
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

    public void Heal(int healValue)
    {
        Health += healValue;
        Health = Mathf.Clamp(Health , 0 , MaxHealth);
        OnHeal?.Invoke();
    }
    
    #endregion

    #region Getters & Setters
    
    public int Health
    {
        get => health;
        set
        {
            health = value;
            OnHealthChange?.Invoke((float)Health / MaxHealth);
        }
    }
    
    #endregion
}
