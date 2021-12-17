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

        #region MonoBehaviour Functions
        private void Start()
        {
            photonView.RPC("StartStartingCutscene" , RpcTarget.All);
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
                startCutsceneObj.SetActive(false);
            }
        }

        private void LoadNextLevel()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
        
        #region Event Functions

        private void OnStartCutsceneFinished()
        {
            CreatePlayer();
            startCutsceneObj.SetActive(false);
        }

        private void OnWinCutsceneFinished()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnWin()
        {
            winCutsceneObj.SetActive(true);
        }
        
        [PunRPC]
        private void StartStartingCutscene()
        {
            startCutsceneObj.SetActive(true);
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.StartCutsceneFinishedEvent , OnStartCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.WinCutsceneFinishedEvent , OnWinCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.StartCutsceneFinishedEvent , OnStartCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinCutsceneFinishedEvent , OnWinCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        #endregion
    }
}
