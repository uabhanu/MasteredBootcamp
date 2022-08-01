using UnityEngine;

namespace AI
{
    public class DefaultEnemyAI : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private AIBehaviour patrolBehaviour;
        [SerializeField] private AIBehaviour shootBehaviour;
        [SerializeField] private AIDetector aiDetector;
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
