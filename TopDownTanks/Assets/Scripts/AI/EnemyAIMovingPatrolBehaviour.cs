using System.Collections;
using UnityEngine;

namespace AI
{
    public class EnemyAIMovingPatrolBehaviour : EnemyAIBehaviour
    {
        #region Variables
        
        private bool _bIsInitialized;
        private bool _bIsWaiting;
        private int _currentIndex = -1;
        private Quaternion _defaultTurretRotation;
        private TankTurretHandler _turretHandler;

        [Range(0.1f , 1.0f)] [SerializeField] private float arriveDistance = 1f;
        [SerializeField] private float waitTime = 0.5f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private Vector2 currentPatrolTargetPos = Vector2.zero;
        
        #endregion
        
        #region Functions

        private void Awake()
        {
            if(patrolPath == null)
            {
                patrolPath = GetComponentInChildren<PatrolPath>();
            }

            if(_turretHandler == null)
            {
                _turretHandler = GetComponentInChildren<TankTurretHandler>();
                _defaultTurretRotation = _turretHandler.transform.rotation;
            }
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(waitTime);
            var nextPathPoint = patrolPath.GetNextPathPoint(_currentIndex);
            _currentIndex = nextPathPoint.Index;
            currentPatrolTargetPos = nextPathPoint.Position;
            _bIsWaiting = false;
        }

        private void CalculateCurrentPathPoint(TankController targetTankController)
        {
            if(!_bIsInitialized)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(targetTankController.transform.position);
                _currentIndex = currentPathPoint.Index;
                currentPatrolTargetPos = currentPathPoint.Position;
                _bIsInitialized = true;
            }
        }

        private void CalculateDirection(TankController targetTankController)
        {
            Vector2 directionToMove = currentPatrolTargetPos - (Vector2)targetTankController.TankBodyMover.transform.position;
            var dotProduct = Vector2.Dot(targetTankController.TankBodyMover.transform.up , directionToMove.normalized);

            if(dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(targetTankController.TankBodyMover.transform.up , directionToMove.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                targetTankController.HandleMoveBody(new Vector2(rotationResult , 1));
            }
            else
            {
                targetTankController.HandleMoveBody(Vector2.up);
            }
        }

        private void CalculateDistance(TankController targetTankController)
        {
            if(Vector2.Distance(targetTankController.transform.position , currentPatrolTargetPos) < arriveDistance)
            {
                _bIsWaiting = true;
                StartCoroutine(WaitCoroutine());
            }
        }
        
        private void ResetTurretRotation()
        {
            var patrollingEnemyObj = gameObject;
            var tankController = patrollingEnemyObj.GetComponentInChildren<TankController>();

            if(tankController == null)
            {
                return;
            }

            if(patrollingEnemyObj.GetComponentInChildren<Rigidbody2D>().velocity.y > 0)
            {
                _turretHandler.transform.localRotation = _defaultTurretRotation;
            }
        }

        public override void PerformAction(TankController targetTankController , AIDetector aiDetector)
        {
            if(!_bIsWaiting)
            {
                if(patrolPath.Length < 2)
                {
                    return;
                }

                if(targetTankController != null)
                {
                    CalculateCurrentPathPoint(targetTankController);
                    CalculateDirection(targetTankController);
                    CalculateDistance(targetTankController);
                    ResetTurretRotation();
                }
            }
        }
        
        #endregion
    }
}
