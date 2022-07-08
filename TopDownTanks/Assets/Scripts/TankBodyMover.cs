using UnityEngine;

public class TankBodyMover : MonoBehaviour
{
    #region Variable Declarations

    private Vector2 _movementVector;

    [SerializeField] private float accelaration = 75f;
    [SerializeField] private float currentSpeed = 0f;
    [SerializeField] private float currentForwardDirection = 1f;
    [SerializeField] private float deaccelaration = 80f;
    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Rigidbody2D tankBody2D;

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
        tankBody2D.velocity = (Vector2)transform.up * (currentSpeed * currentForwardDirection * Time.fixedDeltaTime);
        tankBody2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    private void CalculateCurrentSpeed(Vector2 moveVector)
    {
        if(Mathf.Abs(moveVector.y) > 0)
        {
            currentSpeed += accelaration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deaccelaration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed , 0 , maxMoveSpeed);
    }

    public void Move(Vector2 moveVector)
    {
        _movementVector = moveVector;
        CalculateCurrentSpeed(_movementVector); // If necessary, pass moveVector instead

        if(_movementVector.y > 0)
        {
            currentForwardDirection = 1f;
        }
        
        else if(_movementVector.y < 0)
        {
            currentForwardDirection = -1f;
        }
    }

    #endregion
}
