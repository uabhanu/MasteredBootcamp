using UnityEngine;

namespace AI
{
    public abstract class AIBehaviour : MonoBehaviour
    { 
        public abstract void PerformAction(TankController tankController , AIDetector aiDetector);
    }
}
