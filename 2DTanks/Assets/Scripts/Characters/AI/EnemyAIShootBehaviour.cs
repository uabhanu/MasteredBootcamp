using UnityEngine;

namespace Characters.AI
{
    public class EnemyAIShootBehaviour : AIBehaviour
    {
        #region Variable Declarations
        
        public float FieldOfVisionForShooting = 60f;
        
        #endregion
        
        #region Functions
        
        private bool TargetInFOV(AIDetector aiDetector , TurretPlaceholder turretPlaceholder)
        {
            if(aiDetector.VisibleTargetTransform != null)
            {
                Vector3 direction = aiDetector.VisibleTargetTransform.position - turretPlaceholder.transform.position;
                
                if(Vector2.Angle(turretPlaceholder.transform.right , direction) < FieldOfVisionForShooting / 2)
                {
                    return true;
                }
            }

            return false;
        }
        
        public override void PerformAction(AIDetector aiDetector , Tank tank , TurretPlaceholder turretPlaceholder)
        {
            if(aiDetector.VisibleTargetTransform != null)
            {
                turretPlaceholder.HandleTurretMovement(aiDetector.VisibleTargetTransform.position);   
            }

            if(TargetInFOV(aiDetector , turretPlaceholder))
            {
                tank.HandleMoveBody(Vector2.zero);
                turretPlaceholder.HandleShoot();
            }
        }
        #endregion
    }
}
