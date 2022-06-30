using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BulletData" , menuName = "ScriptableObjects/BulletData")]
    public class BulletData : ScriptableObject
    {
        public float MaxDistance = 10f;
        public float Speed = 100f;
        public int Damage = 5;
    }
}
