using UnityEngine;

namespace AI
{
    public class AIShootBehaviour : AIBehaviour
    {
        #region Variables
        
        public float FieldOfVisionForShooting = 60f;
        
        #endregion

        #region Functions
        
        private bool TargetInFOV(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                var direction = aiDetector.Target.position - tankController.AimTurret.transform.position;

                if(Vector2.Angle(tankController.AimTurret.transform.right , direction) < FieldOfVisionForShooting / 2)
                {
                    return true;
                }   
            }

            return false;
        }
        
        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                if(TargetInFOV(tankController , aiDetector))
                {
                    tankController.HandleMoveBody(Vector2.zero);
                    tankController.HandleShoot();
                }
            
                tankController.HandleMoveTurret(aiDetector.Target.position);   
            }
        }
        
        #endregion
    }
}
