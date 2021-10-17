using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class RobberData : ScriptableObject
    {
        [SerializeField] private float wanderDistance;
        [SerializeField] private float wanderJitter;
        [SerializeField] private float wanderRadius;
        [SerializeField] private Vector3 wanderTarget;

        public float WanderDistance
        {
            get => wanderDistance;
            set => wanderDistance = value;
        }
        
        public float WanderJitter
        {
            get => wanderJitter;
            set => wanderJitter = value;
        }

        public float WanderRadius
        {
            get => wanderRadius;
            set => wanderRadius = value;
        }

        public Vector3 WanderTarget
        {
            get => wanderTarget;
            set => wanderTarget = value;
        }
    }
}
