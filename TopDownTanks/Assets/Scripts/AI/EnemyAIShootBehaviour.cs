using UnityEngine;

namespace AI
{
    public class EnemyAIShootBehaviour : EnemyAIBehaviour
    {
        #region Variables
        
        
        [SerializeField] private float fieldOfVisionForShooting = 60f;

        #endregion
        
        #region Functions

        private bool TargetInFOV(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                if(aiDetector.Target != null)
                {
                    var direction = aiDetector.Target.position - tankController.TankTurretHandler.transform.position;
                
                    if(Vector2.Angle(tankController.TankTurretHandler.transform.right , direction) < fieldOfVisionForShooting / 2)
                    {
                        return true;
                    }
                }   
            }
            
            return false;
        }

        public override void PerformAction(TankController targetTankController , AIDetector aiDetector)
        {
            if(aiDetector == null || targetTankController == null)
            {
                return;
            }
            
            if(TargetInFOV(targetTankController , aiDetector))
            {
                targetTankController.HandleMoveBody(Vector2.zero);
                targetTankController.HandleShoot();
            }

            if(aiDetector.Target != null)
            {
                targetTankController.HandleMoveTurret(aiDetector.Target.position);   
            }
        }
        
        #endregion
    }
}
