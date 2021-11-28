using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class SoundManagerData : ScriptableObject
    {
        [SerializeField] private AudioClip gameOverClip;
        [SerializeField] private AudioClip heartBeatClip;

        public AudioClip GameOverClip
        {
            get => gameOverClip;
            set => gameOverClip = value;
        }
        
        public AudioClip HeartBeatClip
        {
            get => heartBeatClip;
            set => heartBeatClip = value;
        }
    }
}
