using System.IO;
using BhanuAssets.Scripts.ScriptableObjects;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        
        private void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            //Transform Position is the GameManager Position and not the Player Prefab.
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , transform.position , Quaternion.identity);
        }
    }
}
