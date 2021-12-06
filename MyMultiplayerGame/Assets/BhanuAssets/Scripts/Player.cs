using BhanuAssets.Scripts.ScriptableObjects;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        private Material materialToUse;
        private MeshRenderer playerRenderer;

        [SerializeField] private PlayerData playerData;

        private void Start()
        {
            playerRenderer = GetComponent<MeshRenderer>();
            
            if(PhotonNetwork.IsMasterClient)
            {
                materialToUse = playerData.LocalMaterial;
                playerRenderer.material = materialToUse;
            }
            else
            {
                materialToUse = playerData.RemoteMaterial;
                playerRenderer.material = materialToUse;
            }
        }
    }
}
