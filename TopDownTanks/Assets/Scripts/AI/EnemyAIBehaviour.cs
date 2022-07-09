using UnityEngine;

namespace AI
{
    public abstract class EnemyAIBehaviour : MonoBehaviour
    {
        public abstract void PerformAction(TankController tankController , AIDetector aiDetector);
    }
}
