using UnityEngine;

public class TankMover : MonoBehaviour
{
    #region Variables
    
    private Vector2 _movementVector;

    public float Acceleration = 70f;
    public float CurrentForwardDirection = 1f;
    public float CurrentSpeed = 0f;
    public float Deacceleration = 50f;
    public float MaxSpeed = 70f;
    public float RotationSpeed = 200f;
    public Rigidbody2D Rb2d;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        Rb2d = GetComponentInParent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Rb2d.velocity = (Vector2)transform.up * (CurrentForwardDirection * CurrentSpeed * Time.fixedDeltaTime);
        Rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * RotationSpeed * Time.fixedDeltaTime));
    }

    private void CalculateCurrentSpeed(Vector2 movementVector)
    {
        if(Mathf.Abs(movementVector.y) > 0)
        {
            CurrentSpeed += Acceleration * Time.deltaTime;
        }
        else
        {
            CurrentSpeed -= Deacceleration * Time.deltaTime;
        }

        CurrentSpeed = Mathf.Clamp(CurrentSpeed , 0f , MaxSpeed);
    }

    public void Move(Vector2 movementVector)
    {
        _movementVector = movementVector;
        CalculateCurrentSpeed(movementVector);

        if(movementVector.y > 0)
        {
            CurrentForwardDirection = 1f;
        }
        
        else if(movementVector.y < 0)
        {
            CurrentForwardDirection = -1f;
        }
    }
    
    #endregion
}
