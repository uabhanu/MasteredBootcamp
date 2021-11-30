using Bhanu;
using Events;
using Photon.Pun;

namespace BhanuAssets.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            LogMessages.AllIsWellMessage("Connecting to Master :)");
            PhotonNetwork.ConnectUsingSettings(); //Connect Using the settings that you can find in the Resources folder or by Photon->Highlight Server Settings
        }

        public override void OnConnectedToMaster()
        {
            LogMessages.AllIsWellMessage("Connected to Master :)");
            PhotonNetwork.JoinLobby(); //This is where you find or create rooms
            EventsManager.InvokeEvent(BhanuEvent.ConnectedToMasterEvent);
        }

        public override void OnJoinedLobby()
        {
            EventsManager.InvokeEvent(BhanuEvent.JoinedLobbyEvent);
            LogMessages.AllIsWellMessage("Joined Lobby :)");
        }
    }
    
    //Use Connected, Joined, etc events
}
