using Events;
using UnityEngine;

public class PlayerTank : MonoBehaviour
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
    
    private void OnMoveBody(Vector2 movementVector)
    {
        Debug.Log(movementVector);
    }

    private void OnMoveTurret(Vector2 mouseVector)
    {
        Debug.Log(mouseVector);
    }

    private void OnShoot()
    {
        Debug.Log("Player Pressed Shoot Button");
    }
    
    #endregion

    #region Event Listeners

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToPlayerInputEvent(PlayerInputEvent.InputEventMoveBody , OnMoveBody);
        EventsManager.SubscribeToPlayerInputEvent(PlayerInputEvent.InputEventMoveTurret , OnMoveTurret);
        EventsManager.SubscribeToPlayerInputEvent(PlayerInputEvent.InputEventShoot , OnShoot);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromPlayerInputEvent(PlayerInputEvent.InputEventMoveBody , OnMoveBody);
        EventsManager.UnsubscribeFromPlayerInputEvent(PlayerInputEvent.InputEventMoveTurret , OnMoveTurret);
        EventsManager.UnsubscribeFromPlayerInputEvent(PlayerInputEvent.InputEventShoot , OnShoot);
    }

    #endregion
}
