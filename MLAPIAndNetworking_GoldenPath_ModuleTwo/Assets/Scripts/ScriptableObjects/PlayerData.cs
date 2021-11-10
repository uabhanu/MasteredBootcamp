using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private Material clientPlayerMaterial;
        [SerializeField] private Material hostPlayerMaterial;

        public Material ClientPlayerMaterial
        {
            get => clientPlayerMaterial;
            set => clientPlayerMaterial = value;
        }
        
        public Material HostPlayerMaterial
        {
            get => hostPlayerMaterial;
            set => hostPlayerMaterial = value;
        }
    }
}
