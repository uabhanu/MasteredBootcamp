using Events;
using Photon.Pun;

namespace BhanuAssets.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
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
