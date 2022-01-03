using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            //PlayerPrefs.DeleteAll(); //This is for testing only
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}