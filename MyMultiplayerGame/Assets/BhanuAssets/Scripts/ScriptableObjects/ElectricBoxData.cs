using UnityEngine;

namespace Assets.BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class ElectricBoxData : ScriptableObject
    {
        [SerializeField] private bool playerCollided;

        public bool PlayerCollided { get => playerCollided; set => playerCollided = value; }
    }
}