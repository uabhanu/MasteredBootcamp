using UnityEngine;

namespace DataSo
{
    [CreateAssetMenu(fileName = "TurretDataSo" , menuName = "TankData/TurretDataSo")]
    public class TurretDataSo : ScriptableObject
    {
        public BulletDataSo BulletDataSo;
        public GameObject BulletPrefab;
        public float ReloadDelay = 1f;
    }
}
