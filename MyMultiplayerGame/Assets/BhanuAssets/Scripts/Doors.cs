using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Doors : MonoBehaviour
    {
        private bool doorOpen = false;
        
        [SerializeField] private Animator anim;
        
        #region MonoBehaviour Functions

        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        #endregion

        public void OpenDoors()
        {
            doorOpen = true;
            anim.SetBool("DoorTriggered" , doorOpen);
            EventsManager.InvokeEvent(BhanuEvent.WinCutsceneStartedEvent);
        }
        
        #region Event Functions

        private void OnWin()
        {
            OpenDoors();
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.WinEvent , OnWin);
        }
        
        #endregion
    }
}
