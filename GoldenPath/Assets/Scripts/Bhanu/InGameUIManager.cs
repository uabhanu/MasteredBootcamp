using Unity.Netcode;
using UnityEngine;

namespace Bhanu
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject clientMenuCanvasObj;
        [SerializeField] private GameObject hostMenuCanvasObj;
        [SerializeField] private GameObject mainMenuCanvasObj;
        [SerializeField] private GameObject serverMenuCanvasObj;

        private void Start()
        {
            if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                DisplayButtons();
            }
        }

        private void DisplayButtons()
        {
            mainMenuCanvasObj.SetActive(true);
        }

        public void ClientButton()
        {
            clientMenuCanvasObj.SetActive(true);
            
            hostMenuCanvasObj.SetActive(false);
            mainMenuCanvasObj.SetActive(false);
            serverMenuCanvasObj.SetActive(false);

            NetworkManager.Singleton.StartClient();
        }

        public void HostButton()
        {
            hostMenuCanvasObj.SetActive(true);
            
            clientMenuCanvasObj.SetActive(false);
            mainMenuCanvasObj.SetActive(false);
            serverMenuCanvasObj.SetActive(false);

            NetworkManager.Singleton.StartHost();
        }

        public void MoveButton()
        {
            if(NetworkManager.Singleton.IsServer)
            {
                NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();   
            }
        }

        public void RequestChangePositionButton()
        {
            if(NetworkManager.Singleton.IsClient)
            {
                NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();   
            }
        }

        public void ServerButton()
        {
            serverMenuCanvasObj.SetActive(true);
            
            clientMenuCanvasObj.SetActive(false);
            hostMenuCanvasObj.SetActive(false);
            mainMenuCanvasObj.SetActive(false);

            NetworkManager.Singleton.StartServer();
        }

        public void Shutdown()
        {
            mainMenuCanvasObj.SetActive(true);
            
            clientMenuCanvasObj.SetActive(false);
            hostMenuCanvasObj.SetActive(false);
            serverMenuCanvasObj.SetActive(false);
            
            NetworkManager.Singleton.Shutdown();
        }
    }
}
