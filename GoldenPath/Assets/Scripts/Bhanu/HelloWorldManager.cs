using Unity.Netcode;
using UnityEngine;

namespace Bhanu
{
    public class HelloWorldManager : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10 , 10 , 300 , 300));
            
            if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        private static void StartButtons()
        {
            if(GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if(GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
            if(GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

        private static void StatusLabels()
        {
            string mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        private static void SubmitNewPosition()
        {
            if(GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
                player.Move();
            }
        }
    }
}