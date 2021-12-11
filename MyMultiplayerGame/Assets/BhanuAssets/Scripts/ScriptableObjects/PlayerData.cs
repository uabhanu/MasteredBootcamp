using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int electricBoxesCollided = 0;
        [SerializeField] private Material localMaterial;
        [SerializeField] private Material remoteMaterial;
        
        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
        
        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }

        public GameObject Prefab
        {
            get => prefab;
            set => prefab = value;
        }

        public Material LocalMaterial
        {
            get => localMaterial;
            set => localMaterial = value;
        }

        public Material RemoteMaterial
        {
            get => remoteMaterial;
            set => remoteMaterial = value;
        }

        public int ElectricBoxesCollided
        {
            get => electricBoxesCollided;
            set => electricBoxesCollided = value;
        }
    }
}
