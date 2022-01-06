using Photon.Pun;
using System.Collections;
using Bhanu;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class RoomController : MonoBehaviourPunCallbacks
    {
        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private GameObject lobbyMenuObj;
        [SerializeField] private GameObject notEnoughPlayersObj;
        [SerializeField] private GameObject playersListingPrefab;
        [SerializeField] private GameObject roomMenuObj;
        [SerializeField] private GameObject startButtonObj;
        [SerializeField] private int sceneIndex;
        [SerializeField] private LobbyController lobbyController;
        [SerializeField] private TMP_Text roomNameDisplayTMP;
        [SerializeField] private Transform playersDisplayTransform;
        
        #endregion
        
        #region Photon Callback Functions
        
        public override void OnJoinedRoom()
        {
            lobbyMenuObj.SetActive(false);
            roomMenuObj.SetActive(true);
            roomNameDisplayTMP.text = PhotonNetwork.CurrentRoom.Name;

            if(PhotonNetwork.IsMasterClient)
            {
                startButtonObj.SetActive(true);
            }
            else
            {
                startButtonObj.SetActive(false);
            }

            ClearPlayerListings();
            ListPlayers();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            ClearPlayerListings();
            ListPlayers();
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            ClearPlayerListings();
            ListPlayers();

            if(PhotonNetwork.IsMasterClient)
            {
                startButtonObj.SetActive(true);
            }
        }
        
        #endregion
        
        #region User Functions
        
        private IEnumerator ReJoinLobby()
        {
            yield return new WaitForSeconds(1f);
            PhotonNetwork.JoinLobby();
        }
        
        private void ClearPlayerListings()
        {
            for(int i = playersDisplayTransform.childCount - 1; i >= 0; i--)
            {
                Destroy(playersDisplayTransform.GetChild(i).gameObject);
            }
        }

        private void ListPlayers()
        {
            foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GameObject playerListingObj = Instantiate(playersListingPrefab , playersDisplayTransform);
                TMP_Text tempDisplay = playerListingObj.transform.GetChild(0).GetComponent<TMP_Text>();
                tempDisplay.text = player.NickName;
            }
        }
        
        public void LeaveButton()
        {
            lobbyMenuObj.SetActive(true);
            roomMenuObj.SetActive(false);
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.LeaveRoom();
            StartCoroutine(ReJoinLobby());
        }

        public void OKButton()
        {
            notEnoughPlayersObj.SetActive(false);
        }

        public void StartButton()
        {
            if(PhotonNetwork.PlayerList.Length == 2)
            {
                LogMessages.AllIsWellMessage("Both Players Joined :)");
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel(sceneIndex);
            }
            else
            {
                LogMessages.ErrorMessage("We need one more player :)");
                notEnoughPlayersObj.SetActive(true);
            }
        }
        
        #endregion
    }
}