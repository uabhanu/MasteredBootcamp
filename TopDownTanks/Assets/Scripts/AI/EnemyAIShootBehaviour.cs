using UnityEngine;

namespace AI
{
    public class EnemyAIShootBehaviour : EnemyAIBehaviour
    {
        #region Variable Declarations
        
        [SerializeField] private float fieldOfVisionForShooting = 60f;

        #endregion
        
        #region Functions

        private bool TargetInFOV(TankController tankController , AIDetector aiDetector)
        {
            if(aiDetector.Target != null)
            {
                var direction = aiDetector.Target.position - tankController.TankTurretHandler.transform.position;
                
                if(Vector2.Angle(tankController.TankTurretHandler.transform.right , direction) < fieldOfVisionForShooting / 2)
                {
                    return true;
                }
            }

            return false;
        }

        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(TargetInFOV(tankController , aiDetector))
            {
                tankController.HandleMoveBody(Vector2.zero);
                tankController.HandleShoot();
            }

            if(aiDetector.Target != null)
            {
                tankController.HandleMoveTurret(aiDetector.Target.position);   
            }
        }
        
        #endregion
    }
}
