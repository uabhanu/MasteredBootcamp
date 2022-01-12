using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using Photon.Pun;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.IO;
using Bhanu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BhanuAssets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _pipeInTheSocket;
        [SerializeField] private GameObject[] _totalPipeObjs;
        [SerializeField] private List<bool> _listOfPipesInside;

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private GameObject startCutsceneObj;
        [SerializeField] private GameObject winCutsceneObj;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Socket socket;

        #endregion

        #region MonoBehaviour Functions

        private void Start()
        {
            SubscribeToEvents();
            _totalPipeObjs = GameObject.FindGameObjectsWithTag("Pipe");
            CreatePlayer();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        #endregion
        
        #region User Functions

        private void CreatePlayer()
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            
            if(levelIndex == 3)
            {
                PlayerPositioner playerPositioner = GameObject.Find("PlayerPositioner").GetComponent<PlayerPositioner>();
                int randomIndex = Random.Range(0 , playerPositioner.SpawnPositions.Length);
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , playerPositioner.SpawnPositions[randomIndex] , Quaternion.identity).GetComponent<PhotonView>();
            }
            else
            {
                Vector3 spawnPos = new Vector3(Random.Range(-4f , 4f) , transform.position.y , Random.Range(0f , 3.89f));
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , spawnPos , Quaternion.identity).GetComponent<PhotonView>();
            }

            if(startCutsceneObj != null)
            {
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

        private void OnAllElectricBoxesCollided()
        {
            if(winCutsceneObj != null && !winCutsceneObj.activeSelf)
            {
                //LogMessages.AllIsWellMessage("You AllElectricBoxesCollided :)");
                winCutsceneObj.SetActive(true);
            }
        }

        private void OnDeath()
        {
            CreatePlayer();
        }
        
        private void OnPipeInTheSocket()
        {
            _pipeInTheSocket = true;
            
            _listOfPipesInside.Add(_pipeInTheSocket);    
               
            if(_listOfPipesInside.Count == _totalPipeObjs.Length)
            {
                EventsManager.InvokeEvent(BhanuEvent.AllSocketsFilled);
            }
        }

        private void OnPipeNoLongerInTheSocket()
        {
            _listOfPipesInside.Remove(_pipeInTheSocket);
        }

        private void OnStartCutsceneFinished()
        {
            CreatePlayer();
            startCutsceneObj.SetActive(false);
            playerData.StartCutsceneWatched = true;
        }

        private void OnWinCutsceneFinished()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
        
        #region Event Listeners

        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.AllElectricBoxesCollided , OnAllElectricBoxesCollided);
            EventsManager.SubscribeToEvent(BhanuEvent.Death , OnDeath);
            EventsManager.SubscribeToEvent(BhanuEvent.PipeInTheSocket , OnPipeInTheSocket);
            EventsManager.SubscribeToEvent(BhanuEvent.PipeNoLongerInTheSocket , OnPipeNoLongerInTheSocket);
            EventsManager.SubscribeToEvent(BhanuEvent.StartCutsceneFinished , OnStartCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.WinCutsceneFinished , OnWinCutsceneFinished);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.AllElectricBoxesCollided , OnAllElectricBoxesCollided);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.Death , OnDeath);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.PipeInTheSocket , OnPipeInTheSocket);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.PipeNoLongerInTheSocket , OnPipeNoLongerInTheSocket);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.StartCutsceneFinished , OnStartCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinCutsceneFinished , OnWinCutsceneFinished);
        }
        
        #endregion
    }
}
