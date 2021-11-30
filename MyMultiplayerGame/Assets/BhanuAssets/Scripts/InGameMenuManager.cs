using System;
using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class InGameMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject loadingMenuObj;
        [SerializeField] private GameObject titleMenuObj;
        
        private void Start()
        {
            SubscribeToEvents();
            titleMenuObj.SetActive(false);
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        public void CreateRoomButton()
        {
            
        }

        public void FindRoomButton()
        {
            
        }

        public void QuitButton()
        {
            
        }
        
        #region Event Functions
        private void OnJoinedLobby()
        {
            loadingMenuObj.SetActive(false);
            titleMenuObj.SetActive(true);
        }
        #endregion
        
        #region Event Listeners
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.JoinedLobbyEvent , OnJoinedLobby);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.JoinedLobbyEvent , OnJoinedLobby);
        }
        #endregion
    }
    
}
