using Unity.Netcode;
using UnityEngine;

public class RPCTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if(IsClient)
        {
            TestServerRpc(0);
        }
    }

    [ClientRpc]
    private void TestClientRpc(int value)
    {
        if(IsClient)
        {
            Debug.Log("Client Received the RPC #" + value);
            TestServerRpc(value + 1);
        }
    }

    [ServerRpc]
    private void TestServerRpc(int value)
    {
        Debug.Log("Server Received the RPC #" + value);
        TestClientRpc(value);
    }
}