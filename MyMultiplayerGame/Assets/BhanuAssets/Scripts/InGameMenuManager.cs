using Bhanu;
using Events;
using Photon.Pun;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BhanuAssets.Scripts
{
    public class InGameMenuManager : MonoBehaviour
    {
        [SerializeField] private bool noInternet;
        
        #region Serialized Private Variables Declarations
        
        [SerializeField] private GameObject createRoomMenuObj;
        [SerializeField] private GameObject creatingRoomMenuObj;
        [SerializeField] private GameObject errorMenuObj;
        [SerializeField] private GameObject findingRoomMenuObj;
        [SerializeField] private GameObject leavingRoomMenuObj;
        [SerializeField] private GameObject loadingMenuObj;
        [SerializeField] private GameObject mainMenuObj;
        [SerializeField] private GameObject okButtonObj;
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
            ResetAll();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        #endregion

        private void ResetAll()
        {
            createRoomMenuObj.SetActive(false);
            creatingRoomMenuObj.SetActive(false);
            errorMenuObj.SetActive(false);
            findingRoomMenuObj.SetActive(false);
            leavingRoomMenuObj.SetActive(false);
            roomMenuObj.SetActive(false);
            timerObj.SetActive(false);
            titleMenuObj.SetActive(false);
            tryAgainButtonObj.SetActive(false);
        }

        #region Button Functions
        
        public void BackButton()
        {
            if(!noInternet)
            {
                createRoomMenuObj.SetActive(false);
                titleMenuObj.SetActive(true);   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
        }

        public void CreateButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.CreateRoomRequestEvent);
        }

        public void CreateRoomButton()
        {
            if(!noInternet)
            {
                createRoomMenuObj.SetActive(true);
                titleMenuObj.SetActive(false);   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
        }

        public void ExitButton()
        {
            if(!noInternet)
            {
                mainMenuObj.SetActive(true);
                titleMenuObj.SetActive(false);   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
        }

        public void FindRoomButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.FindRoomEvent);
        }

        public void LeaveRoomButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.LeaveRoomRequestEvent);
        }

        public void OKButton()
        {
            if(!noInternet)
            {
                createRoomMenuObj.SetActive(true);
                errorMenuObj.SetActive(false);
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        #endregion
        
        #region Event Functions
        
        private void OnConnectedToInternet()
        {
            noInternet = false;
        }
        
        private void OnConnectedToMaster()
        {
            LogMessages.AllIsWellMessage("Connected to Master :)");
            PhotonNetwork.JoinLobby(); //This is where you find or create rooms
        }

        private void OnConnectingToMaster()
        {
            mainMenuObj.SetActive(false);
            
            if(!noInternet)
            {
                loadingMenuObj.SetActive(true);
                LogMessages.WarningMessage("Connecting to Master :)");
                PhotonNetwork.ConnectUsingSettings(); //Connect Using the settings that you can find in the Resources folder or by Photon->Highlight Server Settings   
            }
            else
            {
                titleMenuObj.SetActive(true);
                LogMessages.AllIsWellMessage("Connected to Master Already :)");
            }
        }
        
        private void OnCreateRoomFailed()
        {
            errorMenuObj.SetActive(true);
            LogMessages.ErrorMessage("Unable to create the room :(");
        }
        
        private void OnCreateRoomRequested()
        {
            createRoomMenuObj.SetActive(false);
            
            if(!noInternet)
            {
                if(string.IsNullOrEmpty(roomNameInputField.text))
                {
                    LogMessages.ErrorMessage("Please Enter a name for your room before trying to create one :)");
                    errorMenuObj.SetActive(true);
                    errorTMP.text = "Please Enter a name for your room before trying to create one :)";
                }
                else
                {
                    creatingRoomMenuObj.SetActive(true);
                    PhotonNetwork.CreateRoom(roomNameInputField.text);   
                }   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
        }

        private void OnFindingRoom()
        {
            if(!noInternet)
            {
                findingRoomMenuObj.SetActive(true);
                titleMenuObj.SetActive(false);   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }

            //Photon Network's Find Room Function here
        }
        
        private void OnFindRoom()
        {
            EventsManager.InvokeEvent(BhanuEvent.FindingRoomEvent);
        }
        
        private void OnJoinedLobby()
        {
            if(!noInternet)
            {
                LogMessages.AllIsWellMessage("Joined Lobby :)");
                loadingMenuObj.SetActive(false);
                titleMenuObj.SetActive(true);   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
        }

        private void OnJoinedRoom()
        {
            if(!noInternet)
            {
                LogMessages.AllIsWellMessage("Joined Room :)");
                createRoomMenuObj.SetActive(false);
                creatingRoomMenuObj.SetActive(false);
                roomMenuObj.SetActive(true);
                roomNameTMP.text = "Room " + roomNameInputField.text;   
            }
            else
            {
                LogMessages.ErrorMessage("Sir Bhanu, No Internet :(");
            }
        }

        private void OnLeaveRoomRequest()
        {
            leavingRoomMenuObj.SetActive(true);
            roomMenuObj.SetActive(false);
            EventsManager.InvokeEvent(BhanuEvent.LeavingRoomEvent);
        }

        private void OnLeavingRoom()
        {
            PhotonNetwork.LeaveRoom();
            leavingRoomMenuObj.SetActive(true);
            timerObj.SetActive(true);
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
            createRoomMenuObj.SetActive(false);
            creatingRoomMenuObj.SetActive(false);
            errorMenuObj.SetActive(true);
            errorTMP.text = "Unable to connect to the Internet :( Please make sure your internet connection is active :)";
            leavingRoomMenuObj.SetActive(false);
            LogMessages.ErrorMessage("On No Internet Event :(");
            noInternet = true;
            okButtonObj.SetActive(false);
            timerObj.SetActive(false);
            titleMenuObj.SetActive(false);
            tryAgainButtonObj.SetActive(true);
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.ConnectedToInternetEvent , OnConnectedToInternet);
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
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.ConnectedToInternetEvent , OnConnectedToInternet);
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
        }
        
        #endregion
    }
    
}
