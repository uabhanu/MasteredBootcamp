using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewTurretData" , menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        public BulletData BulletData;
        public float ReloadDelay;
        public GameObject BulletPrefab;
    }
}
