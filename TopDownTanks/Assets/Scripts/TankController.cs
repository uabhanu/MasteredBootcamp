using UnityEngine;

public class TankController : MonoBehaviour
{
    #region Variable Declarations

    private Vector2 _movementVector;

    [SerializeField] private TankTurret[] tankTurretsArray;
    
    public TankBodyMover TankBodyMover;
    public TankTurretHandler TankTurretHandler;

    #endregion

    #region Functions

    private void Awake()
    {
        if(TankBodyMover == null)
        {
            TankBodyMover = GetComponentInChildren<TankBodyMover>();
        }
        
        if(TankTurretHandler == null)
        {
            TankTurretHandler = GetComponentInChildren<TankTurretHandler>();
        }

        if(tankTurretsArray == null || tankTurretsArray.Length == 0)
        {
            tankTurretsArray = GetComponentsInChildren<TankTurret>();
        }
    }

    public void HandleShoot()
    {
        foreach(var turret in tankTurretsArray)
        {
            turret.Shoot();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        TankBodyMover.Move(movementVector);
    }

    public void HandleMoveTurret(Vector2 mousePointerPos)
    {
        TankTurretHandler.Aim(mousePointerPos);
    }

    #endregion
}
