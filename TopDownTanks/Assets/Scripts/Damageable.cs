using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Util;

public class Damageable : MonoBehaviour
{
    #region Variables
    
    private int _maxHealth = 100;

    [SerializeField] private float disappearTime;
    [SerializeField] private GameObject healthBarObj;
    [SerializeField] private int health = 0;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UnityEvent onDead;
    [SerializeField] private UnityEvent<float> onHealthChange;
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

    private IEnumerator DisappearCoroutine(GameObject gameObjectToDisappear)
    {
        yield return new WaitForSeconds(disappearTime);
        gameObjectToDisappear.SetActive(false);
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
    }
    
    public void Hit(int damageValue)
    {
        Health -= damageValue;

        if(Health <= 0)
        {
            onDead?.Invoke();

            if(scoreManager != null)
            {
                var scoreHelper = GetComponent<ScoreHelper>();
                scoreManager.ScoreUpdate(scoreHelper.ScoreIncrement);   
            }
        }
        else
        {
            onHit?.Invoke();
        }
        
        if(healthBarObj != null)
        {
            StartCoroutine(DisappearCoroutine(healthBarObj));   
        }
    }

    #endregion
}
