using System;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        
        private static event Action BothCylindersCollidedAction;
        private static event Action DeathAction;
        private static event Action ElectricBoxCollidedAction;
        private static event Action ElectricBoxNotCollidedAction;
        private static event Action StartCutsceneFinishedAction;
        private static event Action StartCutsceneStartedAction;
        private static event Action SwordDroppedAction;
        private static event Action SwordInTheSocketAction;
        private static event Action SwordNoLongerInTheSocketAction;
        private static event Action SwordPickedUpAction;
        private static event Action TryAgainAction;
        private static event Action WinCutsceneFinishedAction;
        private static event Action WinCutsceneStartedAction;

        #endregion

        #region Invoke Functions
        
        public static void InvokeEvent(BhanuEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuEvent.BothCylindersCollided:
                    BothCylindersCollidedAction?.Invoke(); 
                return;

                case BhanuEvent.Death:
                    DeathAction?.Invoke(); 
                return;

                case BhanuEvent.CylinderCollided:
                    ElectricBoxCollidedAction?.Invoke(); 
                return;
                
                case BhanuEvent.CylinderNotCollided:
                    ElectricBoxNotCollidedAction?.Invoke(); 
                return;

                case BhanuEvent.StartCutsceneFinished:
                    StartCutsceneFinishedAction?.Invoke(); 
                return;
                
                case BhanuEvent.StartCutsceneStarted:
                    StartCutsceneStartedAction?.Invoke(); 
                return;
                
                case BhanuEvent.SwordDropped:
                    SwordDroppedAction?.Invoke(); 
                return;
                
                case BhanuEvent.SwordInTheSocket:
                    SwordInTheSocketAction?.Invoke(); 
                return;
                
                case BhanuEvent.SwordNoLongerInTheSocket:
                    SwordNoLongerInTheSocketAction?.Invoke(); 
                return;
                
                case BhanuEvent.SwordPickedUp:
                    SwordPickedUpAction?.Invoke(); 
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
            }
        }

        #endregion

        #region Subscribe To Events
        
        public static void SubscribeToEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.BothCylindersCollided:
                    BothCylindersCollidedAction += actionFunction;
                return;

                case BhanuEvent.Death:
                    DeathAction += actionFunction;
                return;

                case BhanuEvent.CylinderCollided:
                    ElectricBoxCollidedAction += actionFunction;
                return;
                
                case BhanuEvent.CylinderNotCollided:
                    ElectricBoxNotCollidedAction += actionFunction;
                return;

                case BhanuEvent.StartCutsceneFinished:
                    StartCutsceneFinishedAction += actionFunction;
                return;
                
                case BhanuEvent.StartCutsceneStarted:
                    StartCutsceneStartedAction += actionFunction;
                return;
                
                case BhanuEvent.SwordDropped:
                    SwordDroppedAction += actionFunction;
                return;
                
                case BhanuEvent.SwordInTheSocket:
                    SwordInTheSocketAction += actionFunction;
                return;
                
                case BhanuEvent.SwordNoLongerInTheSocket:
                    SwordNoLongerInTheSocketAction += actionFunction;
                return;
                
                case BhanuEvent.SwordPickedUp:
                    SwordPickedUpAction += actionFunction;
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
            }
        }

        #endregion

        #region Unsubscribe From Events
        
        public static void UnsubscribeFromEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.BothCylindersCollided:
                    BothCylindersCollidedAction -= actionFunction;
                return;

                case BhanuEvent.Death:
                    DeathAction -= actionFunction;
                return;

                case BhanuEvent.CylinderCollided:
                    ElectricBoxCollidedAction -= actionFunction;
                return;
                
                case BhanuEvent.CylinderNotCollided:
                    ElectricBoxNotCollidedAction -= actionFunction;
                return;

                case BhanuEvent.StartCutsceneFinished:
                    StartCutsceneFinishedAction -= actionFunction;
                return;
                
                case BhanuEvent.StartCutsceneStarted:
                    StartCutsceneStartedAction -= actionFunction;
                return;
                
                case BhanuEvent.SwordDropped:
                    SwordDroppedAction -= actionFunction;
                return;
                
                case BhanuEvent.SwordInTheSocket:
                    SwordInTheSocketAction -= actionFunction;
                return;
                
                case BhanuEvent.SwordNoLongerInTheSocket:
                    SwordNoLongerInTheSocketAction -= actionFunction;
                return;
                
                case BhanuEvent.SwordPickedUp:
                    SwordPickedUpAction -= actionFunction;
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
            }
        }

        #endregion
    }
}
