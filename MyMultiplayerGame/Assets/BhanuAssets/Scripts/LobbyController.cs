using Bhanu;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class LobbyController : MonoBehaviourPunCallbacks
    {
        #region Private Variables Declarations
        
        private const int _roomSizeValue = 2;
        private List<RoomInfo> _roomListings;
        private string _roomNameValue;
        
        #endregion
        
        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private GameObject backgroundMusicObj;
        [SerializeField] private GameObject loadingObj;
        [SerializeField] private GameObject lobbyMenuObj;
        [SerializeField] private GameObject matchmakingMenuObj;
        [SerializeField] private GameObject noMoreRoomsErrorObj;
        [SerializeField] private GameObject roomCreatedAlreadyErrorObj;
        [SerializeField] private GameObject roomListingPrefab;
        [SerializeField] private GameObject submitButtonObj;
        [SerializeField] private TMP_InputField playerNameInputTMP;
        [SerializeField] private Transform roomsDisplayTransform;

        #endregion

        #region Photon Callback Functions
        
        public override void OnConnectedToMaster()
        {
            backgroundMusicObj.SetActive(true);
            loadingObj.SetActive(false);
            PhotonNetwork.AutomaticallySyncScene = true;
            _roomListings = new List<RoomInfo>();
            submitButtonObj.SetActive(true);

            if(PlayerPrefs.HasKey("Nickname"))
            {
                if(PlayerPrefs.GetString("Nickname") == "")
                {
                    PhotonNetwork.NickName = "Player" + Random.Range(0 , 1000);
                }
                else
                {
                    PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
                }
            }
            else
            {
                PhotonNetwork.NickName = "Player" + Random.Range(0 , 1000); 
            }
            
            playerNameInputTMP.text = PhotonNetwork.NickName;
        }

        public override void OnCreateRoomFailed(short returnCode , string message)
        {
            LogMessages.ErrorMessage("Tried to create room but failed probably because there is already a room with the same name :(");
            roomCreatedAlreadyErrorObj.SetActive(true);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomInfoList)
        {
            int tempIndex;

            foreach(RoomInfo roomInfo in roomInfoList)
            {
                if(_roomListings != null)
                {
                    tempIndex = _roomListings.FindIndex(ByName(roomInfo.Name));
                }
                else
                {
                    tempIndex = -1;
                }
                
                if(roomInfo.PlayerCount > 0)
                {
                    _roomListings.Add(roomInfo);
                    ListRoom(roomInfo);
                }
            }
        }
        
        #endregion

        #region User Functions
        
        public int RoomSizeValue => _roomSizeValue;
        
        public void BackButton()
        {
            lobbyMenuObj.SetActive(false);
            matchmakingMenuObj.SetActive(true);
            PhotonNetwork.LeaveLobby();
        }

        public void CreateRoom()
        {
            if(PhotonNetwork.CountOfRooms == 0)
            {
                RoomOptions roomOptions = new RoomOptions() { IsVisible = true , IsOpen = true , MaxPlayers = _roomSizeValue };
                PhotonNetwork.CreateRoom(_roomNameValue , roomOptions);   
            }
            else
            {
                noMoreRoomsErrorObj.SetActive(true);
            }
        }
        
        private void ListRoom(RoomInfo roomInfo)
        {
            if(roomInfo.IsOpen && roomInfo.IsVisible)
            {
                GameObject tempListingObj = GameObject.FindGameObjectWithTag("RoomList");

                if(tempListingObj == null)
                {
                    tempListingObj = Instantiate(roomListingPrefab , roomsDisplayTransform);
                    RoomButton tempRoomButton = tempListingObj.GetComponent<RoomButton>();
                    tempRoomButton.SetRoom(roomInfo.Name , roomInfo.MaxPlayers , roomInfo.PlayerCount);   
                }
            }
        }

        public void OKButton()
        {
            noMoreRoomsErrorObj.SetActive(false);
            roomCreatedAlreadyErrorObj.SetActive(false);
        }

        public void OnRoomNameChanged(string nameIn)
        {
            _roomNameValue = nameIn;
        }

        public void PlayerNameUpdate(string nameInput)
        {
            PhotonNetwork.NickName = nameInput;
            PlayerPrefs.SetString("Nickname" , nameInput);
            playerNameInputTMP.text = nameInput;
        }

        public void SubmitButton()
        {
            lobbyMenuObj.SetActive(true);
            matchmakingMenuObj.SetActive(false);
            PhotonNetwork.JoinLobby();
        }

        static System.Predicate<RoomInfo> ByName(string name)
        {
            return delegate (RoomInfo roomInfo)
            {
                return roomInfo.Name == name;
            };
        }
        
        #endregion
    }
}
