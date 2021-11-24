using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class SoundManagerData : ScriptableObject
    {
        [SerializeField] private AudioClip dangerClip;

        public AudioClip DangerClip
        {
            get => dangerClip;
            set => dangerClip = value;
        }
    }
}
