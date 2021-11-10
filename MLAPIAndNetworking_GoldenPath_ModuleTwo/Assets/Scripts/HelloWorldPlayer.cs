using ScriptableObjects;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class HelloWorldPlayer : NetworkBehaviour
{
    private Material _materialToUse;
    
    [SerializeField] private PlayerData playerData;
    
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    
    public override void OnNetworkSpawn()
    {
        if(IsClient)
        {
            _materialToUse = playerData.ClientPlayerMaterial;
            gameObject.GetComponent<MeshRenderer>().material = _materialToUse;
        }
        
        if(IsHost)
        {
            _materialToUse = playerData.HostPlayerMaterial;
            gameObject.GetComponent<MeshRenderer>().material = _materialToUse;
        }
        
        if(IsServer)
        {
            _materialToUse = playerData.HostPlayerMaterial;
            gameObject.GetComponent<MeshRenderer>().material = _materialToUse;
        }
        
        if(IsOwner)
        {
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
    private void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        Position.Value = GetRandomPositionOnPlane();
    }

    private static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f , 3f) , 1f , Random.Range(-3f , 3f));
    }

    private void Update()
    {
        transform.position = Position.Value;
    }
}