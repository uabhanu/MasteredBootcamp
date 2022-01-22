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
                photonView.RPC("SkipStartingCutsceneRPC" , RpcTarget.All);
            }
        }

        #endregion
        
        #region User Functions

        [PunRPC]
        private void SkipStartingCutsceneRPC()
        {
            StartingCutSceneEnded();
        }

        public void EndGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void StartingCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.StartCutsceneFinished);
        }

        public void WinCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.WinCutsceneFinished);
        }
        
        #endregion
    }
}
