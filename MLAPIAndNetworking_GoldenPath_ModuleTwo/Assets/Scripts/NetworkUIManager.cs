using Unity.Netcode;
using UnityEngine;

public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] private GameObject clientMenuUIObj;
    [SerializeField] private GameObject hostMenuUIObj;
    [SerializeField] private GameObject mainMenuUIObj;
    [SerializeField] private GameObject serverMenuUIObj;
    private void Start()
    {
        clientMenuUIObj.SetActive(false);
        hostMenuUIObj.SetActive(false);
        mainMenuUIObj.SetActive(false);
        serverMenuUIObj.SetActive(false);
        
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            DisplayButtons();
        }
    }

    private void DisplayButtons()
    {
        mainMenuUIObj.SetActive(true);
    }

    public void ClientButton()
    {
        clientMenuUIObj.SetActive(true);
        hostMenuUIObj.SetActive(false);
        mainMenuUIObj.SetActive(false);
        serverMenuUIObj.SetActive(false);
        
        NetworkManager.Singleton.StartClient();
    }

    public void HostButton()
    {
        clientMenuUIObj.SetActive(false);
        hostMenuUIObj.SetActive(true);
        mainMenuUIObj.SetActive(false);
        serverMenuUIObj.SetActive(false);
        
        NetworkManager.Singleton.StartHost();
    }

    public void MoveButton()
    {
        NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
        player.Move();
    }

    public void RequestPositionChangeButton()
    {
        NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
        player.Move();
    }

    public void ServerButton()
    {
        clientMenuUIObj.SetActive(false);
        hostMenuUIObj.SetActive(false);
        mainMenuUIObj.SetActive(false);
        serverMenuUIObj.SetActive(true);
        
        NetworkManager.Singleton.StartServer();
    }
}
