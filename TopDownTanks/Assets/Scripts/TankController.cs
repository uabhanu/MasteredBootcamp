using UnityEngine;

public class TankController : MonoBehaviour
{
    #region Variable Declarations

    private Vector2 _movementVector;

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float turretRotationSpeed = 110f;
    [SerializeField] private Rigidbody2D tankBody2D;
    [SerializeField] private Transform turretHandler;

    #endregion

    #region Functions

    private void Awake()
    {
        if(tankBody2D == null)
        {
            tankBody2D = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        tankBody2D.velocity = (Vector2)transform.up * (_movementVector.y * maxSpeed * Time.fixedDeltaTime);
        tankBody2D.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    public void HandleShoot()
    {
        Debug.Log("Shooting");
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        _movementVector = movementVector;
    }

    public void HandleMoveTurret(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        
        turretHandler.rotation = Quaternion.RotateTowards(turretHandler.rotation , Quaternion.Euler(0 , 0 , desiredAngle - 90f) , rotationStep);
    }

    #endregion
}
