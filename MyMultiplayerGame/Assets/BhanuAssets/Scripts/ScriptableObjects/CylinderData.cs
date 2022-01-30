using UnityEngine;

namespace Assets.BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class CylinderData : ScriptableObject
    {
        [SerializeField] private bool playerCollided;

        public bool PlayerCollided { get => playerCollided; set => playerCollided = value; }
    }
}