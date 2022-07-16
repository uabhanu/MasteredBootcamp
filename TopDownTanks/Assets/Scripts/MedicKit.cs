using DataSo;
using UnityEngine;
using UnityEngine.Events;
using Util;

[RequireComponent(typeof(DestroyHelper))]
public class MedicKit : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private MedicKitDataSo medicKitDataSo;
    [SerializeField] private UnityEvent onHeal;
    
    #endregion

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        onHeal?.Invoke();

        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Heal(medicKitDataSo.HealValue); 
        }
    }
}
