using Unity.Netcode;
using UnityEngine;

public class NetworkVariableTest : NetworkBehaviour
{
    private float last_t = 0.0f;
    private NetworkVariable<float> _serverNetworkVariable = new NetworkVariable<float>();

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            _serverNetworkVariable.Value = 0.0f;
            //Debug.Log("Server's var initialized to: " + ServerNetworkVariable.Value);
        }
    }

    private void Update()
    {
        float t_now = Time.time;
        
        if(IsServer)
        {
            _serverNetworkVariable.Value = _serverNetworkVariable.Value + 0.1f;
            
            if(t_now - last_t > 0.5f)
            {
                last_t = t_now;
                //Debug.Log("Server set its var to: " + ServerNetworkVariable.Value);
            }
        }
    }
}