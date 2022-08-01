using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewTankMovementData" , menuName = "Data/TankMovementData")]
    public class TankMovementData : ScriptableObject
    {
        public float Acceleration = 70f;
        public float Deacceleration = 50f;
        public float MaxSpeed = 60f;
        public float RotationSpeed = 100f;
    }
}
