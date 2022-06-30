using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TankMovementData" , menuName = "ScriptableObjects/TankData/TankMovementData")]
    public class TankMovementData : ScriptableObject
    {
        public float Acceleration = 80f;
        public float Deacceleration = 80f;
        public float MaxSpeed = 70f;
        public float RotationSpeed = 200f;
    }
}
