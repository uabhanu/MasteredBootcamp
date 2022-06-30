using ScriptableObjects;
using UnityEngine;

public class TankBodyMover : MonoBehaviour
{
    #region Private Variable Declarations

    private float _currentForwardDirection = 1;
    private float _currentSpeed;
    private Vector2 _movementVector;
    
    #endregion
    
    #region Private Serialized Field Variable Declarations
    
    [SerializeField] private Rigidbody2D tankBody2D;
    [SerializeField] private TankMovementData tankMovementDataSo;
    
    #endregion
    
    #region MonoBehaviour Functions

    private void Awake()
    {
        if(tankBody2D == null) // If this is not assigned in the inspector
        {
            tankBody2D = GetComponentInParent<Rigidbody2D>();
        }
    }
    
    private void FixedUpdate()
    {
        tankBody2D.velocity = (Vector2)transform.up * (_currentSpeed * _currentForwardDirection * Time.fixedDeltaTime);
        tankBody2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * tankMovementDataSo.RotationSpeed * Time.fixedDeltaTime));
    }

    #endregion

    #region Custom Functions

    private void CalculateCurrentSpeed(Vector2 movementVector)
    {
        if(Mathf.Abs(movementVector.y) > 0)
        {
            _currentSpeed += tankMovementDataSo.Acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= tankMovementDataSo.Deacceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed , 0 , tankMovementDataSo.MaxSpeed);
    }
    
    public void MoveBody(Vector2 movementVector)
    {
        _movementVector = movementVector;
        CalculateCurrentSpeed(movementVector);
        
        if(movementVector.y > 0)
        {
            _currentForwardDirection = 1;
        }
        
        else if(movementVector.y < 0)
        {
            _currentForwardDirection = -1;
        }
    }
    
    #endregion
}
