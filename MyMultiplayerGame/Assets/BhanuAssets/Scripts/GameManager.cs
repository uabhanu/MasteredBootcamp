using System;
using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using Photon.Pun;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BhanuAssets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Private Variables Declarations
        
        private bool _pipeInTheSocket;
        private GameObject[] _totalPipeObjs;
        private List<bool> _listOfPipesInside;
        private PhotonView _photonView;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private GameObject startCutsceneObj;
        [SerializeField] private GameObject level01WinCutsceneObj;
        [SerializeField] private GameObject level02WinCutsceneObj;
        [SerializeField] private PlayerData playerData;

        #endregion

        #region MonoBehaviour Functions

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            SubscribeToEvents();
            _listOfPipesInside = new List<bool>();
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
        private void LevelCompleteRPC()
        {
            GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");

            for(int i = 0; i < playerObjs.Length; i++)
            {
                playerObjs[i].SetActive(false);
            }
                
            level02WinCutsceneObj.SetActive(true);
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
            if(level01WinCutsceneObj != null && !level01WinCutsceneObj.activeSelf)
            {
                //LogMessages.AllIsWellMessage("You AllElectricBoxesCollided :)");
                level01WinCutsceneObj.SetActive(true);
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
                _photonView.RPC("LevelCompleteRPC" , RpcTarget.All);
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
