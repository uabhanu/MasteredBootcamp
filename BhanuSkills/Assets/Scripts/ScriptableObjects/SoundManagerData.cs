using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class SoundManagerData : ScriptableObject
    {
        [SerializeField] private AudioClip heartBeatClip;

        public AudioClip HeartBeatClip
        {
            get => heartBeatClip;
            set => heartBeatClip = value;
        }
    }
}
