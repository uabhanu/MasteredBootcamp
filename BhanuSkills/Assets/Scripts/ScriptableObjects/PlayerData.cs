using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float moveSpeed;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
    }
}
