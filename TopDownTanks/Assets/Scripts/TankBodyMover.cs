using DataSo;
using UnityEngine;
using UnityEngine.Events;

public class TankBodyMover : MonoBehaviour
{
    #region Variables

    private float _currentSpeed;
    [SerializeField] private float _currentForwardDirection;
    private Vector2 _movementVector;
    
    [SerializeField] private Rigidbody2D tankBody2D;
    [SerializeField] private TankMovementDataSo tankMovementDataSo;
    [SerializeField] private UnityEvent<float> onSpeedChange;

    #endregion

    #region Functions

    private void Awake()
    {
        if(tankBody2D == null)
        {
            tankBody2D = GetComponentInParent<Rigidbody2D>();
        }
    }
    
    private void FixedUpdate()
    {
        tankBody2D.velocity = (Vector2)transform.up * (_currentSpeed * _currentForwardDirection * Time.fixedDeltaTime);
        tankBody2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * tankMovementDataSo.RotationSpeed * Time.fixedDeltaTime));
    }

    private void CalculateCurrentSpeed(Vector2 moveVector)
    {
        if(Mathf.Abs(moveVector.y) > 0)
        {
            _currentSpeed += tankMovementDataSo.Acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= tankMovementDataSo.Deacceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed , 0 , tankMovementDataSo.MaxMoveSpeed);
    }
    
    public float CurrentForwardDirection
    {
        get => _currentForwardDirection;
        set => _currentForwardDirection = value;
    }

    public void Move(Vector2 moveVector)
    {
        _movementVector = moveVector;
        CalculateCurrentSpeed(_movementVector);
        onSpeedChange?.Invoke(_movementVector.magnitude);

        if(_movementVector.y > 0)
        {
            _currentForwardDirection = 1f;
        }
        
        else if(_movementVector.y < 0)
        {
            _currentForwardDirection = -1f;
        }

        else
        {
            _currentForwardDirection = 0f;
        }
    }

    #endregion
}
