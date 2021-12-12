using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class CutScenesManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        public void StartingCutSceneEnded()
        {
            gameManager.IsReadyToPlay = true;
            gameManager.CreatePlayer();
        }
        
        public void StartingCutSceneStarted()
        {
            gameManager.IsReadyToPlay = false;
        }
        
        public void WinCutSceneEnded()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Level02");
            }
        }
        
        public void WinCutSceneStarted()
        {
            gameManager.IsReadyToPlay = false;
        }
    }
}
