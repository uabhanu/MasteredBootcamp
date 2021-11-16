using Bhanu.ScriptableObjects;
using Unity.Netcode;
using UnityEngine;

namespace Bhanu
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        [SerializeField] private PlayerData playerData;
        
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if(IsOwner)
            {
                gameObject.GetComponent<MeshRenderer>().material = playerData.LocalPlayerMaterial;
                Move();
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = playerData.RemotePlayerMaterial;
            }
        }

        public void Move()
        {
            if(NetworkManager.Singleton.IsServer)
            {
                Vector3 randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        private void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }

        private void UpdatePosition()
        {
            transform.position = Position.Value;
        }

        private static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f , 3f) , 1f , Random.Range(-3f , 3f));
        }

        private void Update()
        {
            //transform.position = Position.Value; //Tutorial
            UpdatePosition();
        
            if(NetworkManager.Singleton.IsServer)
            {
                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    LogMessages.AllIsWellMessage("Server : Up Arrow / 'W' Key Pressed :)");
                }
                
                if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    LogMessages.AllIsWellMessage("Server : Down Arrow / 'S' Key Pressed :)");
                }
                
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    LogMessages.AllIsWellMessage("Server : Left Arrow / 'A' Key Pressed :)");
                }
                
                if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    LogMessages.AllIsWellMessage("Server : Right Arrow / 'D' Key Pressed :)");
                }   
            }
            else
            {
                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    LogMessages.AllIsWellMessage("Client : Up Arrow / 'W' Key Pressed :)");
                }
                
                if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    LogMessages.AllIsWellMessage("Client : Down Arrow / 'S' Key Pressed :)");
                }
                
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    LogMessages.AllIsWellMessage("Client : Left Arrow / 'A' Key Pressed :)");
                }
                
                if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    LogMessages.AllIsWellMessage("Client : Right Arrow / 'D' Key Pressed :)");
                }
            }
        }
    }
}