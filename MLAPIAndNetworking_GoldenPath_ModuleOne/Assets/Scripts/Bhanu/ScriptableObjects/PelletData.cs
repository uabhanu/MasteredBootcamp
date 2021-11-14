using UnityEngine;

namespace Bhanu.ScriptableObjects
{
    [CreateAssetMenu]
    public class PelletData : ScriptableObject
    {
        [SerializeField] private float moveSpeed = 0f;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int maxPelletsAllowed = 1;

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

        public int MAXPelletsAllowed
        {
            get => maxPelletsAllowed;
            set => maxPelletsAllowed = value;
        }
    }
}
