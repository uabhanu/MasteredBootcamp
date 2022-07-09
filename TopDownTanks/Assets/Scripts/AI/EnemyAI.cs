using UnityEngine;

namespace AI
{
    public class EnemyAI : MonoBehaviour
    {
        #region Variable Declarations
        
        [SerializeField] private AIDetector aiDetector;
        [SerializeField] private EnemyAIBehaviour patrolBehaviour;
        [SerializeField] private EnemyAIBehaviour shootBehaviour;
        [SerializeField] private TankController tankController;
        
        #endregion

        #region Functions
        
        private void Awake()
        {
            if(aiDetector == null)
            {
                aiDetector = GetComponentInChildren<AIDetector>();
            }
            
            if(tankController == null)
            {
                tankController = GetComponentInChildren<TankController>();
            }
        }

        private void Update()
        {
            if(aiDetector.TargetVisible)
            {
                shootBehaviour.PerformAction(tankController , aiDetector);
            }
            else
            {
                patrolBehaviour.PerformAction(tankController , aiDetector);
            }
        }
        
        #endregion
    }
}
