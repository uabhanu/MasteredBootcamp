using Random = UnityEngine.Random;
using UnityEngine;

namespace AI
{
    public class EnemyAIPatrolStaticBehaviour : EnemyAIBehaviour
    {
        #region Variables
        
        [SerializeField] private float currentPatrolDelay;
        [SerializeField] private float patrolDelay = 4f;
        [SerializeField] private Vector2 randomDirection = Vector2.zero;
        
        #endregion
        
        #region Functions

        private void Awake()
        {
            randomDirection = Random.insideUnitCircle;
        }

        public override void PerformAction(TankController targetTankController , AIDetector aiDetector)
        {
            if(targetTankController != null)
            {
                float angle = Vector2.Angle(targetTankController.TankTurretHandler.transform.right , randomDirection);

                if(currentPatrolDelay <= 0 && (angle < 2))
                {
                    randomDirection = Random.insideUnitCircle;
                    currentPatrolDelay = patrolDelay;
                }
                else
                {
                    if(currentPatrolDelay > 0)
                    {
                        currentPatrolDelay -= Time.deltaTime;
                    }
                    else
                    {
                        targetTankController.HandleMoveTurret((Vector2)targetTankController.TankTurretHandler.transform.position + randomDirection);
                    }
                }   
            }
        }
        
        #endregion
    }
}
