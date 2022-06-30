using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TurretData" , menuName = "ScriptableObjects/TankData/TurretData")]
    public class TurretData : ScriptableObject
    {
        public BulletData BulletDataSo;
        public float ReloadDelay;
        public GameObject BulletPrefab;
    }
}
