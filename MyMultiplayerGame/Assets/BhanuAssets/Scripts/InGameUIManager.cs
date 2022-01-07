using System;
using Bhanu;
using Events;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BhanuAssets.Scripts
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winMenuObj;

        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void ContinueButton()
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnAllSocketsFilled()
        {
            if(!winMenuObj.activeSelf)
            {
                LogMessages.AllIsWellMessage("Level 02 Complete");
                winMenuObj.SetActive(true);   
            }
        }

        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.AllSocketsFilled , OnAllSocketsFilled);
        }

        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.AllSocketsFilled , OnAllSocketsFilled);
        }
    }
}
