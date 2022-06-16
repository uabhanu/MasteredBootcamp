using Events;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region MonoBehaviour Functions
    
    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    #endregion

    #region Player Input Event Functions
    
    private void OnShoot()
    {
        Debug.Log("Player Pressed Shoot Button");
    }
    
    #endregion
    
    #region Event Listeners

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToPlayerInputEvent(PlayerInputEvent.InputEventShoot , OnShoot);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromPlayerInputEvent(PlayerInputEvent.InputEventMoveTurret , OnShoot);
    }

    #endregion
}
