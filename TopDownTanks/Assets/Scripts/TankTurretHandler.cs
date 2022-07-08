using UnityEngine;

public class TankTurretHandler : MonoBehaviour
{
    [SerializeField] private float turretRotationSpeed = 110f;

    public void Aim(Vector2 mousePointerPos)
    {
        var turretDirection = (Vector3)mousePointerPos - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.Euler(0 , 0 , desiredAngle - 90f) , rotationStep);
    }
}
