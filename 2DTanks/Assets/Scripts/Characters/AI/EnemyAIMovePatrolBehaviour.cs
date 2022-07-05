using System.Collections;
using UnityEngine;

namespace Characters.AI
{
    public class EnemyAIMovePatrolBehaviour : AIBehaviour
    {
        #region Variable Declarations

        private bool _bIsInitialized = false;
        private int _currentIndex = -1;
        
        [SerializeField] private bool bIsWaiting = false;
        [SerializeField] private Vector2 currentPatrolTargetPosition = Vector2.zero;
        
        [Range(0.1f , 1.0f)] public float ArriveDistance = 1.0f;
        public float WaitTime = 0.5f;
        public PatrolPath PatrolPath;
        
        #endregion
        
        #region Functions

        private void Awake()
        {
            if(PatrolPath == null)
            {
                PatrolPath = GetComponentInChildren<PatrolPath>();
            }
        }
        
        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(WaitTime);
            var nextPathPoint = PatrolPath.GetNextPathPoint(_currentIndex);
            _currentIndex = nextPathPoint.Index;
            bIsWaiting = false;
        }

        public override void PerformAction(AIDetector aiDetector , Tank tank , TurretPlaceholder turretPlaceholder)
        {
            if(!bIsWaiting)
            {
                Debug.Log("Is Waiting False");
                
                if(PatrolPath.Length < 2)
                {
                    Debug.Log("Patrol Path Length Less Than 2");
                    return;
                }

                if(!_bIsInitialized)
                {
                    Debug.Log("Is Initialized False");
                    var currentPathPoint = PatrolPath.GetClosestPathPoint(tank.transform.position);
                    _currentIndex = currentPathPoint.Index;
                    currentPatrolTargetPosition = currentPathPoint.Position;
                    _bIsInitialized = true;
                }

                float currentDistance = Vector2.Distance(tank.transform.position , currentPatrolTargetPosition);

                if(currentDistance < ArriveDistance)
                {
                    Debug.Log("Distance between tank & current patrol target position less than ArriveDistance");
                    bIsWaiting = true;
                    StartCoroutine(WaitCoroutine());
                    return;
                }

                TankBodyMover tankBodyMover = tank.GetComponentInChildren<TankBodyMover>();
                Vector2 directionToMove = currentPatrolTargetPosition - (Vector2)tankBodyMover.transform.position;
                var dotProduct = Vector2.Dot(tankBodyMover.transform.up , directionToMove.normalized);

                if(dotProduct < 0.98f)
                {
                    Debug.Log("Dot Product : " + dotProduct + " less than 0.98f");
                    var crossProduct = Vector3.Cross(tankBodyMover.transform.up , directionToMove.normalized);
                    int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                    tank.HandleMoveBody(new Vector2(rotationResult , 1));
                }
                else
                {
                    Debug.Log("Dot Product : " + dotProduct + " greater than 0.98f");
                    tank.HandleMoveBody(Vector2.up);
                }
            }
        }

        #endregion
    }
}
