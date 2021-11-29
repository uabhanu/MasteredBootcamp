using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class HealthBarData : ScriptableObject
    {
        [SerializeField] private int maxTime;
        [SerializeField] private Sprite greenSprite;
        [SerializeField] private Sprite redSprite;
        [SerializeField] private Sprite yellowSprite;

        public int MAXTime
        {
            get => maxTime;
            set => maxTime = value;
        }

        public Sprite GreenSprite
        {
            get => greenSprite;
            set => greenSprite = value;
        }
        
        public Sprite RedSprite
        {
            get => redSprite;
            set => redSprite = value;
        }

        public Sprite YellowSprite
        {
            get => yellowSprite;
            set => yellowSprite = value;
        }
    }
}
