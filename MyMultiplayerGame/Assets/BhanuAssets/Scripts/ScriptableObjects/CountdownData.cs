using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class CountdownData : ScriptableObject
    {
        [SerializeField] private int maxTime;

        public int MAXTime
        {
            get => maxTime;
            set => maxTime = value;
        }
    }
}
