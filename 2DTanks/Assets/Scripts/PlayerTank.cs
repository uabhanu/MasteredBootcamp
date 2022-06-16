using Events;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    private Vector2 _movementVector;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody2D tankBody2D;
    
    #region MonoBehaviour Functions
    
    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void FixedUpdate()
    {
        tankBody2D.velocity = (Vector2)transform.up * (_movementVector.y * maxSpeed * Time.fixedDeltaTime);
        tankBody2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    #endregion
    
    #region Player Input Event Functions
    
    private void OnMoveBody(Vector2 movementVector)
    {
        _movementVector = movementVector;
        //Debug.Log("Player Tank Movement : " + movementVector);
    }

    #endregion

    #region Event Listeners

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToPlayerInputEvent(PlayerInputEvent.InputEventMoveBody , OnMoveBody);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromPlayerInputEvent(PlayerInputEvent.InputEventMoveBody , OnMoveBody);
    }

    #endregion
}
