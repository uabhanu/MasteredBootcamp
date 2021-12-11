using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class DoorData : ScriptableObject
    {
        [SerializeField] private Material lockedMaterial;
        [SerializeField] private Material unlockedMaterial;

        public Material LockedMaterial
        {
            get => lockedMaterial;
            set => lockedMaterial = value;
        }

        public Material UnlockedMaterial
        {
            get => unlockedMaterial;
            set => unlockedMaterial = value;
        }
    }
}
