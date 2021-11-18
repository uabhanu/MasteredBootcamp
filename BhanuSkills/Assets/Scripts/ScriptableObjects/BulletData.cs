using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class BulletData : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private GameObject prefab;
        
        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public GameObject Prefab
        {
            get => prefab;
            set => prefab = value;
        }
    }
}