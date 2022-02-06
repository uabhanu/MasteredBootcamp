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
        
        private bool _swordInTheSocket;
        private GameObject[] _totalSwordObjs;
        [SerializeField] private List<bool> _listOfSwordsInside;
        private PhotonView _photonView;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private GameObject startCutsceneObj;
        [SerializeField] private GameObject level01WinCutsceneObj;
        [SerializeField] private GameObject level02WinCutsceneObj;
        [SerializeField] private LevelData levelData;
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
            _listOfSwordsInside = new List<bool>();
            _totalSwordObjs = GameObject.FindGameObjectsWithTag("Sword");
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
            Vector3 spawnPos = new Vector3(levelData.SpawnXPosition , transform.position.y , Random.Range(levelData.SpawnZPosition , levelData.SpawnZPosition + levelData.OffsetZ));
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , spawnPos , Quaternion.identity).GetComponent<PhotonView>();

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
            
            for(int i = 0; i < _totalSwordObjs.Length; i++)
            {
                _totalSwordObjs[i].SetActive(false);
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

        private void OnBothCylindersCollided()
        {
            if(level01WinCutsceneObj != null && !level01WinCutsceneObj.activeSelf)
            {
                //LogMessages.AllIsWellMessage("You BothCylindersCollided :)");
                
                GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");

                for(int i = 0; i < playerObjs.Length; i++)
                {
                    playerObjs[i].SetActive(false);
                }
                
                level01WinCutsceneObj.SetActive(true);
            }
        }

        private void OnDeath()
        {
            CreatePlayer();
        }

        private void OnStartCutsceneFinished()
        {
            CreatePlayer();
            startCutsceneObj.SetActive(false);
            playerData.StartCutsceneWatched = true;
        }
        
        private void OnSwordInTheSocket()
        {
            _swordInTheSocket = true;
            
            _listOfSwordsInside.Add(_swordInTheSocket);

            if(_listOfSwordsInside.Count == _totalSwordObjs.Length)
            {
                _photonView.RPC("LevelCompleteRPC" , RpcTarget.All);
            }
        }

        private void OnSwordNoLongerInTheSocket()
        {
            _listOfSwordsInside.Remove(_swordInTheSocket);
        }

        private void OnWinCutsceneFinished()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
        
        #region Event Listeners

        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.BothCylindersCollided , OnBothCylindersCollided);
            EventsManager.SubscribeToEvent(BhanuEvent.Death , OnDeath);
            EventsManager.SubscribeToEvent(BhanuEvent.StartCutsceneFinished , OnStartCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.SwordInTheSocket , OnSwordInTheSocket);
            EventsManager.SubscribeToEvent(BhanuEvent.SwordNoLongerInTheSocket , OnSwordNoLongerInTheSocket);
            EventsManager.SubscribeToEvent(BhanuEvent.WinCutsceneFinished , OnWinCutsceneFinished);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.BothCylindersCollided , OnBothCylindersCollided);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.Death , OnDeath);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.StartCutsceneFinished , OnStartCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.SwordInTheSocket , OnSwordInTheSocket);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.SwordNoLongerInTheSocket , OnSwordNoLongerInTheSocket);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinCutsceneFinished , OnWinCutsceneFinished);
        }
        
        #endregion
    }
}
