using UnityEngine;

public class TankController : MonoBehaviour
{
    #region Variables

    public AimTurret AimTurret;
    public TankMover TankMover;
    public Transform TurretParent;
    public Turret[] Turrets;

    #endregion

    #region Functions

    private void Awake()
    {
        // In case user doesn't assign this in the inspector
        if(AimTurret == null)
        {
            AimTurret = GetComponentInChildren<AimTurret>();
        }
        
        // In case user doesn't assign this in the inspector
        if(TankMover == null)
        {
            TankMover = GetComponentInChildren<TankMover>();
        }

        // In case user doesn't assign this in the inspector
        if(Turrets == null || Turrets.Length == 0)
        {
            Turrets = GetComponentsInChildren<Turret>();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        TankMover.Move(movementVector);
    }
    
    public void HandleMoveTurret(Vector2 pointerPosition)
    {
        AimTurret.Aim(pointerPosition);
    }
    
    public void HandleShoot()
    {
        foreach(var turret in Turrets)
        {
            turret.Shoot();
        }
    }
    
    #endregion
}
