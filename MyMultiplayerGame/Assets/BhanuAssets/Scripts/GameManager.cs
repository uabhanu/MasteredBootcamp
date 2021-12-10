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
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-4f , 4f) , 1f , Random.Range(0f , 3.89f)) , Quaternion.identity);
        }
    }
}
