using UnityEngine;
using UnityEngine.Events;
using Util;

[RequireComponent(typeof(DestroyHelper))]
public class Coins : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private UnityEvent onCollected;
    
    #endregion

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        onCollected?.Invoke();
    }
}
