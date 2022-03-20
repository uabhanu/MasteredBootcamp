using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class ChatData : ScriptableObject
    {
        [SerializeField] private string playerName;

        public string PlayerName
        {
            get => playerName;
            set => playerName = value;
        }
    }
}
