using Events;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class CutScenesManager : MonoBehaviour
    {
        private GameObject[] _playersInTheScene;
        
        [SerializeField] private GameManager gameManager;

        public void StartingCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.StartCutsceneFinishedEvent);
        }
        
        public void StartingCutSceneStarted()
        {
            EventsManager.InvokeEvent(BhanuEvent.StartCutsceneStartedEvent);
        }
        
        public void WinCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.WinCutsceneFinishedEvent);
        }
        
        public void WinCutSceneStarted()
        {
            EventsManager.InvokeEvent(BhanuEvent.WinCutsceneStartedEvent);
        }
    }
}
