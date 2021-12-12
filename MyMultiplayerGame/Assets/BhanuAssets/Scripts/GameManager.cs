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
        [SerializeField] private bool _startCutsceneFinished = false;
        
        [SerializeField] private GameObject startCutSceneObj;
        [SerializeField] private GameObject winCutSceneObj;
        [SerializeField] private PlayerData playerData;
        
        #region MonoBehaviour Functions
        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        #endregion
        
        #region User Functions
        
        public bool StartCutsceneFinished
        {
            get => _startCutsceneFinished;
            set => _startCutsceneFinished = value;
        }

        public void CreatePlayer()
        {
            if(_startCutsceneFinished)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-4f , 4f) , 1f , Random.Range(0f , 3.89f)) , Quaternion.identity);
                startCutSceneObj.SetActive(false);   
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
            GameObject[] otherPlayersInTheScene = GameObject.FindGameObjectsWithTag("Player");

            if(otherPlayersInTheScene != null)
            {
                for(int i = 0; i < otherPlayersInTheScene.Length; i++)
                {
                    otherPlayersInTheScene[i].SetActive(true);
                }   
            }
            
            StartCutsceneFinished = true;
            startCutSceneObj.SetActive(false);
            CreatePlayer();
        }
        
        private void OnStartCutsceneStarted()
        {
            GameObject[] otherPlayersInTheScene = GameObject.FindGameObjectsWithTag("Player");

            if(otherPlayersInTheScene != null)
            {
                for(int i = 0; i < otherPlayersInTheScene.Length; i++)
                {
                    otherPlayersInTheScene[i].SetActive(false);
                }   
            }

            StartCutsceneFinished = false;
            startCutSceneObj.SetActive(true);
        }
        
        private void OnWinCutsceneFinished()
        {
            
        }

        private void OnWinCutsceneStarted()
        {
            
        }
        
        private void OnWin()
        {
            winCutSceneObj.SetActive(true);
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.StartCutsceneFinishedEvent , OnStartCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.StartCutsceneStartedEvent , OnStartCutsceneStarted);
            EventsManager.SubscribeToEvent(BhanuEvent.WinCutsceneFinishedEvent , OnWinCutsceneFinished);
            EventsManager.SubscribeToEvent(BhanuEvent.WinCutsceneStartedEvent , OnWinCutsceneStarted);
            EventsManager.SubscribeToEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.StartCutsceneFinishedEvent , OnStartCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.StartCutsceneStartedEvent , OnStartCutsceneStarted);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinCutsceneFinishedEvent , OnWinCutsceneFinished);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinCutsceneStartedEvent , OnWinCutsceneStarted);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        #endregion
    }
}
