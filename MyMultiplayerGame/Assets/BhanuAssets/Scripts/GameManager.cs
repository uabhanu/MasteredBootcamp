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
        [SerializeField] private GameObject startCutSceneObj;
        [SerializeField] private GameObject winCutSceneObj;

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

        private void CreatePlayer()
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-4f , 4f) , 1f , Random.Range(0f , 3.89f)) , Quaternion.identity);
            startCutSceneObj.SetActive(false);
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
            startCutSceneObj.SetActive(false);
        }

        private void OnWinCutsceneFinished()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
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
