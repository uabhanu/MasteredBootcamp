using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewBulletData" , menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        public float MaxDistance = 10f;
        public float Speed = 100f;
        public int Damage = 5;
    }
}
