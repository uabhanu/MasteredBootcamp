using Events;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

namespace BhanuAssets.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public override void OnConnected()
        {
            EventsManager.InvokeEvent(BhanuEvent.ConnectedToInternetEvent);
        }

        public override void OnConnectedToMaster()
        {
            EventsManager.InvokeEvent(BhanuEvent.ConnectedToMasterEvent);
        }

        public override void OnCreateRoomFailed(short returnCode , string message)
        {
            EventsManager.InvokeEvent(BhanuEvent.CreateRoomFailedEvent);
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            EventsManager.InvokeEvent(BhanuEvent.NoInternetEvent);
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

        public override void OnRoomListUpdate(List<RoomInfo> roomsList)
        {
            EventsManager.InvokeEvent(BhanuEvent.RoomsListUpdatedEvent); //TODO Find out if this even is getting fired or not
        }
    }
}
