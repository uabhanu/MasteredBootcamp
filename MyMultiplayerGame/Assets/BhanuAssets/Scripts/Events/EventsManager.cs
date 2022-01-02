using System;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        
        private static event Action DeathAction;
        private static event Action ElectricBoxCollidedAction;
        private static event Action ElectricBoxNotCollidedAction;
        private static event Action StartCutsceneFinishedAction;
        private static event Action StartCutsceneStartedAction;
        private static event Action TryAgainAction;
        private static event Action WinCutsceneFinishedAction;
        private static event Action WinCutsceneStartedAction;
        private static event Action WinAction;
        
        #endregion

        #region Invoke Functions
        
        public static void InvokeEvent(BhanuEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuEvent.Death:
                    DeathAction?.Invoke(); 
                return;

                case BhanuEvent.ElectricBoxCollided:
                    ElectricBoxCollidedAction?.Invoke(); 
                return;
                
                case BhanuEvent.ElectricBoxNotCollided:
                    ElectricBoxNotCollidedAction?.Invoke(); 
                return;

                case BhanuEvent.StartCutsceneFinished:
                    StartCutsceneFinishedAction?.Invoke(); 
                return;
                
                case BhanuEvent.StartCutsceneStarted:
                    StartCutsceneStartedAction?.Invoke(); 
                return;

                case BhanuEvent.TryAgain:
                    TryAgainAction?.Invoke(); 
                return;
                
                case BhanuEvent.WinCutsceneFinished:
                    WinCutsceneFinishedAction?.Invoke(); 
                return;
                
                case BhanuEvent.WinCutsceneStarted:
                    WinCutsceneStartedAction?.Invoke(); 
                return;
                
                case BhanuEvent.Win:
                    WinAction?.Invoke(); 
                return;
            }
        }

        #endregion

        #region Subscribe To Events
        
        public static void SubscribeToEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.Death:
                    DeathAction += actionFunction;
                return;

                case BhanuEvent.ElectricBoxCollided:
                    ElectricBoxCollidedAction += actionFunction;
                return;
                
                case BhanuEvent.ElectricBoxNotCollided:
                    ElectricBoxNotCollidedAction += actionFunction;
                return;

                case BhanuEvent.StartCutsceneFinished:
                    StartCutsceneFinishedAction += actionFunction;
                return;
                
                case BhanuEvent.StartCutsceneStarted:
                    StartCutsceneStartedAction += actionFunction;
                return;

                case BhanuEvent.TryAgain:
                    TryAgainAction += actionFunction;
                return;
                
                case BhanuEvent.WinCutsceneFinished:
                    WinCutsceneFinishedAction += actionFunction;
                return;
                
                case BhanuEvent.WinCutsceneStarted:
                    WinCutsceneStartedAction += actionFunction;
                return;
                
                case BhanuEvent.Win:
                    WinAction += actionFunction;
                return;
            }
        }

        #endregion

        #region Unsubscribe From Events
        
        public static void UnsubscribeFromEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.Death:
                    DeathAction -= actionFunction;
                return;

                case BhanuEvent.ElectricBoxCollided:
                    ElectricBoxCollidedAction -= actionFunction;
                return;
                
                case BhanuEvent.ElectricBoxNotCollided:
                    ElectricBoxNotCollidedAction -= actionFunction;
                return;

                case BhanuEvent.StartCutsceneFinished:
                    StartCutsceneFinishedAction -= actionFunction;
                return;
                
                case BhanuEvent.StartCutsceneStarted:
                    StartCutsceneStartedAction -= actionFunction;
                return;

                case BhanuEvent.TryAgain:
                    TryAgainAction -= actionFunction;
                return;
                
                case BhanuEvent.WinCutsceneFinished:
                    WinCutsceneFinishedAction -= actionFunction;
                return;
                
                case BhanuEvent.WinCutsceneStarted:
                    WinCutsceneStartedAction -= actionFunction;
                return;
                
                case BhanuEvent.Win:
                    WinAction -= actionFunction;
                return;
            }
        }

        #endregion
    }
}
