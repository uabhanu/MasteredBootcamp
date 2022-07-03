using UnityEngine;

namespace Characters.AI
{
    public abstract class AIBehaviour : MonoBehaviour
    {
        public abstract void PerformAction(AIDetector aiDetector , Tank tank , TurretPlaceholder turretPlaceholder);
    }
}
