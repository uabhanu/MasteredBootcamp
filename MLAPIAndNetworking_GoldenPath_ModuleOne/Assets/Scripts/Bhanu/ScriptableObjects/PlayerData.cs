using UnityEngine;

namespace Bhanu.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material localMaterial;
        [SerializeField] private Material remoteMaterial;

        public Material DefaultMaterial
        {
            get => defaultMaterial;
            set => defaultMaterial = value;
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
    }
}
