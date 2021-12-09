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
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs" , "PhotonPlayer") , new Vector3(Random.Range(-3.56f , 3.78f) , 0.94f , Random.Range(-3.81f , 2.8f)) , Quaternion.identity);
        }
    }
}
