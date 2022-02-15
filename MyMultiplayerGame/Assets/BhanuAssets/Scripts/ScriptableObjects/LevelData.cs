using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private float offsetX;
        [SerializeField] private float offsetZ;
        [SerializeField] private float spawnXPosition;
        [SerializeField] private float spawnZPosition;
        [SerializeField] private Material socketMaterialGreen;
        [SerializeField] private Material socketMaterialRed;

        public float OffsetX
        {
            get => offsetX;
            set => offsetX = value;
        }
        
        public float OffsetZ
        {
            get => offsetZ;
            set => offsetZ = value;
        }

        public float SpawnXPosition
        {
            get => spawnXPosition;
            set => spawnXPosition = value;
        }

        public float SpawnZPosition
        {
            get => spawnZPosition;
            set => spawnZPosition = value;
        }
    }
}
