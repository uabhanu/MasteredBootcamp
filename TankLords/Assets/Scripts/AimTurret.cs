using UnityEngine;

public class AimTurret : MonoBehaviour
{
    #region Variables
    
    public float TurretRotationSpeed = 150f;
    
    #endregion

    #region Functions

    public void Aim(Vector2 inputPointerPosition)
    {
        var turretDirection = (Vector3)inputPointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = TurretRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.Euler(0 , 0 , desiredAngle) , rotationStep);
    }
    
    #endregion
}
