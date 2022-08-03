using UnityEngine;

namespace AI
{
    public class AIPatrolStaticBehaviour : AIBehaviour
    {
        #region Variables
        
        [SerializeField] private float currentPatrolDelay;
        [SerializeField] private Vector2 randomDirection = Vector2.zero;
        
        public float PatrolDelay = 2.5f;
        
        #endregion

        #region Functions
        
        private void Awake()
        {
            randomDirection = Random.insideUnitCircle;
        }

        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                float angle = Vector2.Angle(tankController.AimTurret.transform.right , randomDirection);

                if(currentPatrolDelay <= 0 && (angle < 2))
                {
                    randomDirection = Random.insideUnitCircle;
                    currentPatrolDelay = PatrolDelay;
                }
                else
                {
                    if(currentPatrolDelay > 0)
                    {
                        currentPatrolDelay -= Time.deltaTime;
                    }
                    else
                    {
                        tankController.HandleMoveTurret((Vector2)tankController.AimTurret.transform.position + randomDirection);
                    }
                }   
            }
        }
        
        #endregion
    }
}
