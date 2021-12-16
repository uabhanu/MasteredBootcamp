using Photon.Realtime;
using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private byte maxPlayers;
        [SerializeField] private string name;

        public byte MaxPlayers
        {
            get => maxPlayers;
            set => maxPlayers = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}
