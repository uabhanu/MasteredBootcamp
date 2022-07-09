using UnityEngine;

namespace DataSo
{
    [CreateAssetMenu(fileName = "TankMovementDataSo" , menuName = "TankData/TankMovementDataSo")]
    public class TankMovementDataSo : ScriptableObject
    {
        public float Acceleration = 75f;
        public float Deacceleration = 65f;
        public float MaxMoveSpeed = 75f;
        public float RotationSpeed = 105f;
    }
}
