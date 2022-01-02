using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class RoomButton : MonoBehaviour
    {
        private int playersCount;
        private int sizeValue;
        private string nameValue;

        [SerializeField] private TMP_Text nameDisplayTMP;
        [SerializeField] private TMP_Text sizeDisplayTMP;

        public void JoinRoomOnClick()
        {
            PhotonNetwork.JoinRoom(nameValue);
            Destroy(gameObject);
        }
        
        public void SetRoom(string nameInput , int sizeInput , int countInput)
        {
            nameValue = nameInput;
            sizeValue = sizeInput;
            playersCount = countInput;
            nameDisplayTMP.text = nameInput;
            sizeDisplayTMP.text = countInput + " / " + sizeInput;
        }
    }
}
