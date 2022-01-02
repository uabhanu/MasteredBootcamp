using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            //Your Code Here
        }
    }
}