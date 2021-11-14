using Bhanu.ScriptableObjects;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bhanu
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        private Material materialToUse;
        
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PelletData pelletData;
        
        public static int numberOfPellets = 0;
        
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
                transform.position = Position.Value;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        public void Shoot()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(numberOfPellets < pelletData.MAXPelletsAllowed)
                {
                    Instantiate(pelletData.Prefab , transform.position , quaternion.identity);
                    numberOfPellets++;
                }
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
            Shoot();
        }
    }
}