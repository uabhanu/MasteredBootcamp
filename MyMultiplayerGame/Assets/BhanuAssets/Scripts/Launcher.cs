using Bhanu;
using Events;
using Photon.Pun;

namespace BhanuAssets.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            LogMessages.WarningMessage("Connecting to Master :)");
            PhotonNetwork.ConnectUsingSettings(); //Connect Using the settings that you can find in the Resources folder or by Photon->Highlight Server Settings
        }

        public override void OnConnectedToMaster()
        {
            EventsManager.InvokeEvent(BhanuEvent.ConnectedToMasterEvent);
        }

        public override void OnCreateRoomFailed(short returnCode , string message)
        {
            EventsManager.InvokeEvent(BhanuEvent.CreateRoomFailedEvent);
        }

        public override void OnJoinedLobby()
        {
            EventsManager.InvokeEvent(BhanuEvent.JoinedLobbyEvent);
        }

        public override void OnJoinedRoom()
        {
            EventsManager.InvokeEvent(BhanuEvent.JoinedRoomEvent);
        }

        public override void OnLeftRoom()
        {
            EventsManager.InvokeEvent(BhanuEvent.LeftRoomEvent);
        }
    }
}
