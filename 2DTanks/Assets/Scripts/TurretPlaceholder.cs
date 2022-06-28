using UnityEngine;

public class TurretPlaceholder : MonoBehaviour
{
    #region Serialized Field Private Variable Declarations
    
    [SerializeField] private float rotationSpeed;

    #endregion
    
    #region Public Variable Declarations
    
    public Turret[] allExistingTurrets; //We don't want to assign the turrets in the inspector
    
    #endregion
    
    #region MonoBehaviour Functions

    private void Awake()
    {
        if(allExistingTurrets == null || allExistingTurrets.Length == 0)
        {
            allExistingTurrets = GetComponentsInChildren<Turret>();
        }
    }
    
    #endregion

    #region Custom Functions
    
    private void Aim(Vector2 inputPointerPosition)
    {
        Vector3 turretDirection = (Vector3)inputPointerPosition - transform.position;
        float desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg; //Convert the angle from Radians to Degrees
        float rotationStep = rotationSpeed * Time.deltaTime;
        
        //Make sure that the Quaternion is starting with 'Q' unless you intend to start with 'q' to avoid bad results
        transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.Euler(0 , 0 , desiredAngle) , rotationStep);
    }
    
    #endregion
    
    #region Unity Event Functions

    public void HandleShoot()
    {
        foreach(Turret turret in allExistingTurrets) //Loop through all the existing turrets
        {
            turret.Shoot();
        }
    }
    
    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        Aim(pointerPosition);   
    }

    #endregion
}
