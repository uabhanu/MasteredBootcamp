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
        [SerializeField] private GameObject winCutSceneObj;
        [SerializeField] private PlayerData playerData;
        
        #region Unity & Other Functions
        private void Start()
        {
            CreatePlayer();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void CreatePlayer()
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-4f , 4f) , 1f , Random.Range(0f , 3.89f)) , Quaternion.identity);
            winCutSceneObj.SetActive(false);
        }

        private void LoadNextLevel()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        #endregion
        
        #region Event Functions

        private void OnWin()
        {
            winCutSceneObj.SetActive(true);
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        #endregion
    }
}
