using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace BhanuAssets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject startCutsceneObj;
        [SerializeField] private GameObject winCutsceneObj;
        [SerializeField] private PhotonView photonView;
        [SerializeField] private PlayerData playerData;

        #region MonoBehaviour Functions

        private void Start()
        {
            if(!playerData.StartCutsceneWatched && PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("StartStartingCutscene" , RpcTarget.All);
            }
            else
            {
                CreatePlayer();
                //photonView.RPC("CreatePlayerRPC" , RpcTarget.All);
            }
            
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        #endregion
        
        #region User Functions

        private void CreatePlayer()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
   
            if(playerObj == null)
            {
                //The 'y' position here is of this current object and not Player Prefab
                playerObj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-4f , 4f) , transform.position.y , Random.Range(0f , 3.89f)) , Quaternion.identity);
                
                if(startCutsceneObj != null)
                {
                    startCutsceneObj.SetActive(false);
                }
            }
        }
        
        [PunRPC]
        private void CreatePlayerRPC()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
   
            if(playerObj == null)
            {
                //The 'y' position here is of this current object and not Player Prefab
                playerObj = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-4f , 4f) , transform.position.y , Random.Range(0f , 3.89f)) , Quaternion.identity);
                startCutsceneObj.SetActive(false);
            }
        }

        private void LoadNextLevel()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        [PunRPC]
        private void StartStartingCutscene()
        {
            if(!playerData.StartCutsceneWatched)
            {
                startCutsceneObj.SetActive(true);
            }
        }

        #endregion
        
        #region Event Functions

        private void OnDeath()
        {
            CreatePlayer();
            //photonView.RPC("CreatePlayerRPC" , RpcTarget.All);
        }

        private void OnStartCutsceneFinished()
        {
            CreatePlayer();
            //photonView.RPC("CreatePlayerRPC" , RpcTarget.All);
            startCutsceneObj.SetActive(false);
            playerData.StartCutsceneWatched = true;
        }

        private void OnWinCutsceneFinished()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnWin()
        {
            if(winCutsceneObj != null)
            {
                winCutsceneObj.SetActive(true);
            }
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.Death , OnDeath);
            EventsManager.SubscribeToEvent(BhanuEvent.StartCutsceneFinished , OnStartCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.WinCutsceneFinished , OnWinCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.Win , OnWin);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.Death , OnDeath);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.StartCutsceneFinished , OnStartCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinCutsceneFinished , OnWinCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.Win , OnWin);
        }
        
        #endregion
    }
}
