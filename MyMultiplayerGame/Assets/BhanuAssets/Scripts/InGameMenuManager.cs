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
        [SerializeField] private bool noInternet = false;
        
        #region Serialized Private Variables Declarations
        
        [SerializeField] private GameObject createRoomMenuObj;
        [SerializeField] private GameObject creatingRoomMenuObj;
        [SerializeField] private GameObject errorMenuObj;
        [SerializeField] private GameObject findingRoomMenuObj;
        [SerializeField] private GameObject leavingRoomMenuObj;
        [SerializeField] private GameObject loadingMenuObj;
        [SerializeField] private GameObject mainMenuObj;
        [SerializeField] private GameObject roomMenuObj;
        [SerializeField] private TMP_Text errorTMP;
        [SerializeField] private GameObject timerObj;
        [SerializeField] private GameObject titleMenuObj;
        [SerializeField] private GameObject tryAgainButtonObj;
        [SerializeField] private TMP_InputField roomNameInputField;
        [SerializeField] private TMP_Text roomNameTMP;

        #endregion
        
        #region Unity Functions
        
        private void Start()
        {
            SubscribeToEvents();
            createRoomMenuObj.SetActive(false);
            creatingRoomMenuObj.SetActive(false);
            errorMenuObj.SetActive(false);
            findingRoomMenuObj.SetActive(false);
            leavingRoomMenuObj.SetActive(false);
            roomMenuObj.SetActive(false);
            timerObj.SetActive(false);
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

        public void ExitButton()
        {
            mainMenuObj.SetActive(true);
            PhotonNetwork.Disconnect();
            titleMenuObj.SetActive(false);
        }

        public void FindRoomButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.FindRoomEvent);
        }

        public void LeaveRoomButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.LeaveRoomRequestEvent);
        }

        public void QuitButton()
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void StartButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.ConnectingToMasterEvent);
        }

        public void TryAgainButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.TryAgainEvent);
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
            mainMenuObj.SetActive(false);
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

            if(string.IsNullOrEmpty(roomNameInputField.text))
            {
                LogMessages.ErrorMessage("Please Enter a name for your room before trying to create one :)");
                errorMenuObj.SetActive(true);
                errorTMP.text = "Please Enter a name for your room before trying to create one :)";
                return;
            }
            else
            {
                creatingRoomMenuObj.SetActive(true);
                PhotonNetwork.CreateRoom(roomNameInputField.text);   
            }
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

        private void OnLeaveRoomRequest()
        {
            leavingRoomMenuObj.SetActive(true);
            roomMenuObj.SetActive(false);
            EventsManager.InvokeEvent(BhanuEvent.LeavingRoomEvent);
        }

        private void OnLeavingRoom()
        {
            if(!noInternet)
            {
                PhotonNetwork.LeaveRoom();
                leavingRoomMenuObj.SetActive(true);
                timerObj.SetActive(true);   
            }
            
            if(noInternet)
            {
                EventsManager.InvokeEvent(BhanuEvent.LeavingRoomFailedEvent);
            }
        }

        private void OnLeavingRoomFailed()
        {
            errorMenuObj.SetActive(true);
            errorTMP.text = "Unable to leave room as there is no internet connection :( Please check your internet connection and try again :)";
            leavingRoomMenuObj.SetActive(false);
            LogMessages.ErrorMessage("No Internet Connection :(");
        }

        private void OnLeftRoom()
        {
            leavingRoomMenuObj.SetActive(false);
            timerObj.SetActive(false);

            if(!noInternet)
            {
                errorMenuObj.SetActive(false);
                titleMenuObj.SetActive(true);   
            }
        }

        private void OnNoInternet()
        {
            errorMenuObj.SetActive(true);
            errorTMP.text = "Unable to leave the room :( Please make sure you have active internet connection and then try again :)";
            leavingRoomMenuObj.SetActive(false);
            tryAgainButtonObj.SetActive(false);
            LogMessages.ErrorMessage("No Internet :(");
            noInternet = true;
        }

        private void OnTryAgain()
        {
            errorMenuObj.SetActive(false);
            createRoomMenuObj.SetActive(true);
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
            EventsManager.SubscribeToEvent(BhanuEvent.LeaveRoomRequestEvent , OnLeaveRoomRequest);
            EventsManager.SubscribeToEvent(BhanuEvent.LeavingRoomEvent , OnLeavingRoom);
            EventsManager.SubscribeToEvent(BhanuEvent.LeavingRoomFailedEvent , OnLeavingRoomFailed);
            EventsManager.SubscribeToEvent(BhanuEvent.LeftRoomEvent , OnLeftRoom);
            EventsManager.SubscribeToEvent(BhanuEvent.NoInternetEvent , OnNoInternet);
            EventsManager.SubscribeToEvent(BhanuEvent.TryAgainEvent , OnTryAgain);
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
            EventsManager.UnsubscribeFromEvent(BhanuEvent.LeaveRoomRequestEvent , OnLeaveRoomRequest);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.LeavingRoomEvent , OnLeavingRoom);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.LeavingRoomFailedEvent , OnLeavingRoomFailed);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.LeftRoomEvent , OnLeftRoom);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.NoInternetEvent , OnNoInternet);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.TryAgainEvent , OnTryAgain);
        }
        
        #endregion
    }
    
}
