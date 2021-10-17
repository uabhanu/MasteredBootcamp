using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class CopData : ScriptableObject
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float rotationSpeed = 100.0f;
        [SerializeField] private float currentSpeed = 0;

        public float CurrentSpeed
        {
            get => currentSpeed;
            set => currentSpeed = value;
        }
        
        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }
        
        public float Speed
        {
            get => speed;
            set => speed = value;
        }
    }
}
