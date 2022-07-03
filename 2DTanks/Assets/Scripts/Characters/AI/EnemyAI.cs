using UnityEngine;

namespace Characters.AI
{
    public class EnemyAI : MonoBehaviour
    {
        #region Serialize Field Private Variable Declerations
        
        [SerializeField] private AIBehaviour patrolBehaviour;
        [SerializeField] private AIBehaviour shootBehaviour;
        [SerializeField] private AIDetector aiDetector;
        [SerializeField] private Tank tank;
        [SerializeField] private TurretPlaceholder turretPlaceholder;
        
        #endregion

        #region MonoBehaviour Functions
        
        private void Awake()
        {
            if(aiDetector == null)
            {
                aiDetector = GetComponentInChildren<AIDetector>();
            }
            
            if(tank == null)
            {
                tank = GetComponentInChildren<Tank>();
            }

            if(turretPlaceholder == null)
            {
                turretPlaceholder = GetComponentInChildren<TurretPlaceholder>();
            }
        }

        private void Update()
        {
            if(aiDetector.TargetVisible)
            {
                shootBehaviour.PerformAction(aiDetector , tank , turretPlaceholder);
            }
            else
            {
                patrolBehaviour.PerformAction(aiDetector , tank , turretPlaceholder);
            }
        }
        
        #endregion
    }
}
