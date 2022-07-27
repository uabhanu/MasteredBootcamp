using UnityEngine;

public class TankController : MonoBehaviour
{
    #region Variables
    
    private Vector2 _movementVector;
    
    public float MaxSpeed = 10f;
    public float RotationSpeed = 100f;
    public float TurretRotationSpeed = 150f;
    public Rigidbody2D Rb2d;
    public Transform TurretParent;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rb2d.velocity = (Vector2)transform.up * (_movementVector.y * MaxSpeed * Time.fixedDeltaTime);
        Rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0 , 0 , -_movementVector.x * RotationSpeed * Time.fixedDeltaTime));
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        _movementVector = movementVector;
    }
    
    public void HandleMoveTurret(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = TurretRotationSpeed * Time.deltaTime;
        TurretParent.rotation = Quaternion.RotateTowards(TurretParent.rotation , Quaternion.Euler(0 , 0 , desiredAngle - 90) , rotationStep);
    }
    
    public void HandleShoot()
    {
        Debug.Log("Shooting");
    }
    
    #endregion
}
