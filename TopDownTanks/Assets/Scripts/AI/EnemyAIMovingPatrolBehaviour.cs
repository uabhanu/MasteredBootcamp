using System.Collections;
using UnityEngine;

namespace AI
{
    public class EnemyAIMovingPatrolBehaviour : EnemyAIBehaviour
    {
        #region Variables

        private bool _isInitialized;
        private int _currentIndex = -1;
        
        [SerializeField] private bool isWaiting;
        [SerializeField] private Vector2 currentPatrolTargetPos = Vector2.zero;
        
        [Range(0.1f , 1.0f)] public float ArriveDistance = 1f;
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
            currentPatrolTargetPos = nextPathPoint.Position;
            isWaiting = false;
        }

        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(!isWaiting)
            {
                if(PatrolPath.Length < 2)
                {
                    return;
                }

                if(!_isInitialized)
                {
                    var currentPathPoint = PatrolPath.GetClosestPathPoint(tankController.transform.position);
                    _currentIndex = currentPathPoint.Index;
                    currentPatrolTargetPos = currentPathPoint.Position;
                    _isInitialized = true;
                }

                if(Vector2.Distance(tankController.transform.position , currentPatrolTargetPos) < ArriveDistance)
                {
                    isWaiting = true;
                    StartCoroutine(WaitCoroutine());
                    return;
                }

                Vector2 directionToMove = currentPatrolTargetPos - (Vector2)tankController.TankBodyMover.transform.position;
                var dotProduct = Vector2.Dot(tankController.TankBodyMover.transform.up , directionToMove.normalized);

                if(dotProduct < 0.98f)
                {
                    var crossProduct = Vector3.Cross(tankController.TankBodyMover.transform.up , directionToMove.normalized);
                    int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                    tankController.HandleMoveBody(new Vector2(rotationResult , 1));
                }
                else
                {
                    tankController.HandleMoveBody(Vector2.up);
                }
            }
        }
        
        #endregion
    }
}
