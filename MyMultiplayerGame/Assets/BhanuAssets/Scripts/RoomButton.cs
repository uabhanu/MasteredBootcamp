using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class RoomButton : MonoBehaviour
    {
        private int _playersCount;
        private int _sizeValue;
        private string _nameValue;

        [SerializeField] private TMP_Text nameDisplayTMP;
        [SerializeField] private TMP_Text sizeDisplayTMP;

        public void JoinRoomOnClick()
        {
            PhotonNetwork.JoinRoom(_nameValue);
        }
        
        public void SetRoom(string nameInput , int sizeInput , int countInput)
        {
            _nameValue = nameInput;
            _sizeValue = sizeInput;
            _playersCount = countInput;
            nameDisplayTMP.text = nameInput;
            sizeDisplayTMP.text = countInput + " / " + sizeInput;
        }
    }
}
