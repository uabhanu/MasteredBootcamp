using Events;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class CutScenesManager : MonoBehaviour
    {
        [SerializeField] private PhotonView photonView;

        #region MonoBehaviour Functions

        private void Update()
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                photonView.RPC("SkipStartingCutscene" , RpcTarget.All);
            }
        }

        #endregion
        
        #region User Functions

        [PunRPC]
        private void SkipStartingCutscene()
        {
            StartingCutSceneEnded();
        }
        
        public void StartingCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.StartCutsceneFinishedEvent);
        }

        public void WinCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.WinCutsceneFinishedEvent);
        }
        
        #endregion
    }
}
