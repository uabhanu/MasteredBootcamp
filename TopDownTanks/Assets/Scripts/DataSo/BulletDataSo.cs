using UnityEngine;

namespace DataSo
{
    [CreateAssetMenu(fileName = "BulletDataSo" , menuName = "BulletData/BulletDataSo")]
    public class BulletDataSo : ScriptableObject
    {
        public float MaxDistance = 10f;
        public float Speed = 10f;
        public int Damage = 20;
    }
}
