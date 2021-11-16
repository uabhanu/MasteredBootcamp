using UnityEngine;

namespace Bhanu.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Material localPlayerMaterial;
        [SerializeField] private Material remotePlayerMaterial;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public Material LocalPlayerMaterial
        {
            get => localPlayerMaterial;
            set => localPlayerMaterial = value;
        }

        public Material RemotePlayerMaterial
        {
            get => remotePlayerMaterial;
            set => remotePlayerMaterial = value;
        }
    }
}
