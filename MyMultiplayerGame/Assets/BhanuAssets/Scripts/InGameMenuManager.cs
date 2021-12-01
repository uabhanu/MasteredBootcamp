using Bhanu;
using Events;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class InGameMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject createRoomMenuObj;
        [SerializeField] private GameObject errorMenuObj;
        [SerializeField] private GameObject loadingMenuObj;
        [SerializeField] private GameObject roomMenuObj;
        [SerializeField] private GameObject titleMenuObj;
        [SerializeField] private TMP_InputField roomNameInputField;
        [SerializeField] private TMP_Text roomNameTMP;
        
        private void Start()
        {
            SubscribeToEvents();
            createRoomMenuObj.SetActive(false);
            errorMenuObj.SetActive(false);
            roomMenuObj.SetActive(false);
            titleMenuObj.SetActive(false);
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #region Button Functions
        public void BackButton()
        {
            createRoomMenuObj.SetActive(false);
            titleMenuObj.SetActive(true);
        }

        public void CreateButton()
        {
            if(string.IsNullOrEmpty(roomNameInputField.text))
            {
                LogMessages.ErrorMessage("Please Enter your Name to Continue");
                return;
            }

            PhotonNetwork.CreateRoom(roomNameInputField.text);
        }

        public void CreateRoomButton()
        {
            createRoomMenuObj.SetActive(true);
            titleMenuObj.SetActive(false);
        }

        public void FindRoomButton()
        {
            
        }

        public void LeaveRoomButton()
        {
            PhotonNetwork.LeaveRoom();
            loadingMenuObj.SetActive(true);
            roomMenuObj.SetActive(false);
        }

        public void QuitButton()
        {
            
        }
        #endregion
        
        #region Event Functions
        private void OnConnectedToMaster()
        {
            LogMessages.AllIsWellMessage("Connected to Master :)");
            PhotonNetwork.JoinLobby(); //This is where you find or create rooms
        }
        private void OnCreateRoomFailed()
        {
            errorMenuObj.SetActive(true);
            LogMessages.ErrorMessage("Unable to create the room :(");
        }
        private void OnCreateRoomRequested()
        {
            createRoomMenuObj.SetActive(false);
            loadingMenuObj.SetActive(true);
        }
        private void OnJoinedLobby()
        {
            LogMessages.AllIsWellMessage("Joined Lobby :)");
            loadingMenuObj.SetActive(false);
            titleMenuObj.SetActive(true);
        }

        private void OnJoinedRoom()
        {
            LogMessages.AllIsWellMessage("Joined Room :)");
            createRoomMenuObj.SetActive(false);
            loadingMenuObj.SetActive(false);
            roomMenuObj.SetActive(true);
            roomNameTMP.text = "Room " + roomNameInputField.text;
        }

        private void OnLeftRoom()
        {
            titleMenuObj.SetActive(true);
        }
        #endregion //TODO Loading Screen not showing before going to the Room Menu so fix that
        
        #region Event Listeners
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.ConnectedToMasterEvent , OnConnectedToMaster);
            EventsManager.SubscribeToEvent(BhanuEvent.CreateRoomFailedEvent , OnCreateRoomFailed);
            EventsManager.SubscribeToEvent(BhanuEvent.CreateRoomRequestEvent , OnCreateRoomRequested);
            EventsManager.SubscribeToEvent(BhanuEvent.JoinedLobbyEvent , OnJoinedLobby);
            EventsManager.SubscribeToEvent(BhanuEvent.JoinedRoomEvent , OnJoinedRoom);
            EventsManager.SubscribeToEvent(BhanuEvent.LeftRoomEvent , OnLeftRoom);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.ConnectedToMasterEvent , OnConnectedToMaster);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.CreateRoomFailedEvent , OnCreateRoomFailed);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.CreateRoomRequestEvent , OnCreateRoomRequested);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.JoinedLobbyEvent , OnJoinedLobby);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.JoinedRoomEvent , OnJoinedRoom);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.LeftRoomEvent , OnLeftRoom);
        }
        #endregion
    }
    
}
