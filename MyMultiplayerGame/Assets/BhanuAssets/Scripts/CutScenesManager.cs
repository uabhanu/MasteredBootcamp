using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class CutScenesManager : MonoBehaviour
    {
        public void StartingCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.StartCutsceneFinishedEvent);
        }

        public void WinCutSceneEnded()
        {
            EventsManager.InvokeEvent(BhanuEvent.WinCutsceneFinishedEvent);
        }
    }
}
