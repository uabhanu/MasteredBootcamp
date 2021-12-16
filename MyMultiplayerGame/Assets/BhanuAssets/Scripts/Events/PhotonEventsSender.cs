using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

namespace BhanuAssets.Scripts.Events
{
    public class PhotonEventsSender
    {
        private const byte RoomNameEventCode = 0;
        
        public static void SendRoomNameEvent(TMP_InputField roomNameInput)
        {
            string roomName = roomNameInput.text;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(RoomNameEventCode , roomName , raiseEventOptions , SendOptions.SendReliable);
        }
    }
}
