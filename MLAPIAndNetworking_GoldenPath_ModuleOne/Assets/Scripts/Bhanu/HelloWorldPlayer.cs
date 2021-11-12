using Bhanu.ScriptableObjects;
using Unity.Netcode;
using UnityEngine;

namespace Bhanu
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        private Material materialToUse;
        
        [SerializeField] private PlayerData playerData;
        
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            materialToUse = playerData.DefaultMaterial;
            gameObject.GetComponent<MeshRenderer>().material = materialToUse;
            
            if(IsOwner)
            {
                materialToUse = playerData.LocalMaterial;
                gameObject.GetComponent<MeshRenderer>().material = materialToUse;
                Move();
            }
            else
            {
                materialToUse = playerData.RemoteMaterial;
                gameObject.GetComponent<MeshRenderer>().material = materialToUse;
                Move();
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
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f , 3f) , 1f , Random.Range(-3f , 3f));
        }

        private void Update()
        {
            transform.position = Position.Value;
        }
    }
}