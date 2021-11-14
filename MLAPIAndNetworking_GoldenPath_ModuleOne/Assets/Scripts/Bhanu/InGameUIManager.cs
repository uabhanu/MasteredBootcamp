using Unity.Netcode;
using UnityEngine;

namespace Bhanu
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject clientMenuObj;
        [SerializeField] private GameObject hostMenuObj;
        [SerializeField] private GameObject mainMenuObj;
        [SerializeField] private GameObject serverMenuObj;

        private void Start()
        {
            clientMenuObj.SetActive(false);
            hostMenuObj.SetActive(false);
            mainMenuObj.SetActive(false);
            serverMenuObj.SetActive(false);
            
            if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                DisplayButtons();
            }
        }

        private void DisplayButtons()
        {
            mainMenuObj.SetActive(true);
        }

        public void ClientButton()
        {
            clientMenuObj.SetActive(true);
            hostMenuObj.SetActive(false);
            mainMenuObj.SetActive(false);
            serverMenuObj.SetActive(false);
            
            NetworkManager.Singleton.StartClient();
        }

        public void HostButton()
        {
            clientMenuObj.SetActive(false);
            hostMenuObj.SetActive(true);
            mainMenuObj.SetActive(false);
            serverMenuObj.SetActive(false);
            
            NetworkManager.Singleton.StartHost();
        }
        
        public void MoveButton()
        {
            if(GameManager.PlayerExists())
            {
                BhanuLogMessages.PlayerExistsMessage();
                NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();   
            }
            else
            {
                BhanuLogMessages.NoPlayerExistsMessage();
            }
        }
        
        public void RequestChangePositionButton()
        {
            if(GameManager.PlayerExists())
            {
                BhanuLogMessages.PlayerExistsMessage();
                NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();   
            }
            else
            {
                BhanuLogMessages.NoPlayerToRequestChangePositionMessage();        
            }
        }

        public void ServerButton()
        {
            clientMenuObj.SetActive(false);
            hostMenuObj.SetActive(false);
            mainMenuObj.SetActive(false);
            serverMenuObj.SetActive(true);
            
            NetworkManager.Singleton.StartClient();
        }

        public void StopClientButton()
        {
            clientMenuObj.SetActive(false);
            hostMenuObj.SetActive(false);
            mainMenuObj.SetActive(true);
            serverMenuObj.SetActive(false);
            
            NetworkManager.Singleton.Shutdown();
        }
        
        public void StopHostButton()
        {
            clientMenuObj.SetActive(false);
            hostMenuObj.SetActive(false);
            mainMenuObj.SetActive(true);
            serverMenuObj.SetActive(false);
            
            NetworkManager.Singleton.Shutdown();
        }
        
        public void StopServerButton()
        {
            clientMenuObj.SetActive(false);
            hostMenuObj.SetActive(false);
            mainMenuObj.SetActive(true);
            serverMenuObj.SetActive(false);
            
            NetworkManager.Singleton.Shutdown();
        }
    }
}
