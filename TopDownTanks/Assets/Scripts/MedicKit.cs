using UnityEngine;
using UnityEngine.Events;

public class MedicKit : MonoBehaviour
{
    [SerializeField] private UnityEvent onHeal;

    public int HealValue;

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        onHeal?.Invoke();

        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Heal(HealValue); 
        }
    }
}
