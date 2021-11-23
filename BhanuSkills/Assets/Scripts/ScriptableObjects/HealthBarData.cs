using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class HealthBarData : ScriptableObject
    {
        [SerializeField] private int maxTime;

        public int MAXTime
        {
            get => maxTime;
            set => maxTime = value;
        }
    }
}
