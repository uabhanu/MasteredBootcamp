using Random = UnityEngine.Random;
using UnityEngine;

namespace Characters.AI
{
    public class EnemyAIStaticPatrolBehaviour : AIBehaviour
    {
        private float _currentPatrolDelay;
        
        [SerializeField] private Vector2 randomDirection = Vector2.zero;

        public float PatrolDelay = 4f;


        private void Awake()
        {
            //At first this didn't work so I added using System and then it worked, however, this is not a part of System class so this is weird
            randomDirection = Random.insideUnitCircle; 
        }

        public override void PerformAction(AIDetector aiDetector , Tank tank , TurretPlaceholder turretPlaceholder)
        {
            // If this doesn't work, check if AimTurret Code and TurretPlaceholder are same
            // Using Vector2.right as that is the default direction
            float desiredAngle = Vector2.Angle(turretPlaceholder.transform.right , randomDirection);

            if(_currentPatrolDelay <= 0 && desiredAngle < 2)
            {
                randomDirection = Random.insideUnitCircle;
                _currentPatrolDelay = PatrolDelay;
            }
            else
            {
                if(_currentPatrolDelay > 0)
                {
                    _currentPatrolDelay -= Time.deltaTime;
                }
                else
                {
                    turretPlaceholder.HandleTurretMovement((Vector2)transform.position + randomDirection);
                }
            }
        }
    }
}
