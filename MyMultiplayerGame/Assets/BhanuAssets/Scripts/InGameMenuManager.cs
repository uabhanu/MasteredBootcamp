using Bhanu;
using Events;
using Photon.Pun;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class InGameMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject createRoomMenuObj;
        [SerializeField] private GameObject creatingRoomMenuObj;
        [SerializeField] private GameObject errorMenuObj;
        [SerializeField] private GameObject findingRoomMenuObj;
        [SerializeField] private GameObject loadingMenuObj;
        [SerializeField] private GameObject roomMenuObj;
        [SerializeField] private GameObject titleMenuObj;
        [SerializeField] private TMP_InputField roomNameInputField;
        [SerializeField] private TMP_Text roomNameTMP;
        
        #region Unity Functions
        
        private void Start()
        {
            SubscribeToEvents();
            EventsManager.InvokeEvent(BhanuEvent.ConnectingToMasterEvent);
            createRoomMenuObj.SetActive(false);
            creatingRoomMenuObj.SetActive(false);
            errorMenuObj.SetActive(false);
            findingRoomMenuObj.SetActive(false);
            roomMenuObj.SetActive(false);
            titleMenuObj.SetActive(false);
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        #endregion

        #region Button Functions
        
        public void BackButton()
        {
            createRoomMenuObj.SetActive(false);
            titleMenuObj.SetActive(true);
        }

        public void CreateButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.CreateRoomRequestEvent);
        }

        public void CreateRoomButton()
        {
            createRoomMenuObj.SetActive(true);
            titleMenuObj.SetActive(false);
        }

        public void FindRoomButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.FindRoomEvent);
        }

        public void LeaveRoomButton()
        {
            PhotonNetwork.LeaveRoom();
            loadingMenuObj.SetActive(true);
            roomMenuObj.SetActive(false);
        }

        public void QuitButton()
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        #endregion
        
        #region Event Functions
        
        private void OnConnectedToMaster()
        {
            LogMessages.AllIsWellMessage("Connected to Master :)");
            PhotonNetwork.JoinLobby(); //This is where you find or create rooms
        }

        private void OnConnectingToMaster()
        {
            loadingMenuObj.SetActive(true);
            LogMessages.WarningMessage("Connecting to Master :)");
            PhotonNetwork.ConnectUsingSettings(); //Connect Using the settings that you can find in the Resources folder or by Photon->Highlight Server Settings
        }
        
        private void OnCreateRoomFailed()
        {
            errorMenuObj.SetActive(true);
            LogMessages.ErrorMessage("Unable to create the room :(");
        }
        
        private void OnCreateRoomRequested()
        {
            createRoomMenuObj.SetActive(false);
            creatingRoomMenuObj.SetActive(true);
            
            if(string.IsNullOrEmpty(roomNameInputField.text))
            {
                LogMessages.ErrorMessage("Please Enter your Name to Continue");
                return;
            }

            PhotonNetwork.CreateRoom(roomNameInputField.text);
        }

        private void OnFindingRoom()
        {
            findingRoomMenuObj.SetActive(true);
            titleMenuObj.SetActive(false);
            
            //Photon Network's Find Room Function here
        }
        
        private void OnFindRoom()
        {
            EventsManager.InvokeEvent(BhanuEvent.FindingRoomEvent);
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
            creatingRoomMenuObj.SetActive(false);
            roomMenuObj.SetActive(true);
            roomNameTMP.text = "Room " + roomNameInputField.text;
        }

        private void OnLeftRoom()
        {
            titleMenuObj.SetActive(true);
        }
        
        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.ConnectedToMasterEvent , OnConnectedToMaster);
            EventsManager.SubscribeToEvent(BhanuEvent.ConnectingToMasterEvent , OnConnectingToMaster);
            EventsManager.SubscribeToEvent(BhanuEvent.CreateRoomFailedEvent , OnCreateRoomFailed);
            EventsManager.SubscribeToEvent(BhanuEvent.CreateRoomRequestEvent , OnCreateRoomRequested);
            EventsManager.SubscribeToEvent(BhanuEvent.FindingRoomEvent , OnFindingRoom);
            EventsManager.SubscribeToEvent(BhanuEvent.FindRoomEvent , OnFindRoom);
            EventsManager.SubscribeToEvent(BhanuEvent.JoinedLobbyEvent , OnJoinedLobby);
            EventsManager.SubscribeToEvent(BhanuEvent.JoinedRoomEvent , OnJoinedRoom);
            EventsManager.SubscribeToEvent(BhanuEvent.LeftRoomEvent , OnLeftRoom);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.ConnectedToMasterEvent , OnConnectedToMaster);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.ConnectingToMasterEvent , OnConnectingToMaster);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.CreateRoomFailedEvent , OnCreateRoomFailed);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.CreateRoomRequestEvent , OnCreateRoomRequested);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.FindingRoomEvent , OnFindingRoom);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.FindRoomEvent , OnFindRoom);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.JoinedLobbyEvent , OnJoinedLobby);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.JoinedRoomEvent , OnJoinedRoom);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.LeftRoomEvent , OnLeftRoom);
        }
        
        #endregion
    }
    
}
