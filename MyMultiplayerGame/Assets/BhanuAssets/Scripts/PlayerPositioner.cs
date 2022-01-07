using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class PlayerPositioner : MonoBehaviour
    {
        [SerializeField] private Vector3[] spawnPositions;

        public Vector3[] SpawnPositions
        {
            get => spawnPositions;
            set => spawnPositions = value;
        }
    }
}
