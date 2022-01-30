using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private float offset;
        [SerializeField] private float spawnXPosition;
        [SerializeField] private float spawnZPosition;

        public float Offset
        {
            get => offset;
            set => offset = value;
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
