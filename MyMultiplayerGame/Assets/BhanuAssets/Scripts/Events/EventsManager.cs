using System;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        
        private static event Action AllElectricBoxesCollidedAction;
        private static event Action AllSocketsFilledAction;
        private static event Action DeathAction;
        private static event Action ElectricBoxCollidedAction;
        private static event Action ElectricBoxNotCollidedAction;
        private static event Action StartCutsceneFinishedAction;
        private static event Action PipeDroppedAction;
        private static event Action PipeInTheSocketAction;
        private static event Action PipeNoLongerInTheSocketAction;
        private static event Action PipePickedUpAction;
        private static event Action StartCutsceneStartedAction;
        private static event Action TryAgainAction;
        private static event Action WinCutsceneFinishedAction;
        private static event Action WinCutsceneStartedAction;

        #endregion

        #region Invoke Functions
        
        public static void InvokeEvent(BhanuEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuEvent.AllElectricBoxesCollided:
                    AllElectricBoxesCollidedAction?.Invoke(); 
                return;
                
                case BhanuEvent.AllSocketsFilled:
                    AllSocketsFilledAction?.Invoke(); 
                return;
                
                case BhanuEvent.Death:
                    DeathAction?.Invoke(); 
                return;

                case BhanuEvent.ElectricBoxCollided:
                    ElectricBoxCollidedAction?.Invoke(); 
                return;
                
                case BhanuEvent.ElectricBoxNotCollided:
                    ElectricBoxNotCollidedAction?.Invoke(); 
                return;
                
                case BhanuEvent.PipeDropped:
                    PipeDroppedAction?.Invoke(); 
                return;
                
                case BhanuEvent.PipeInTheSocket:
                    PipeInTheSocketAction?.Invoke(); 
                return;
                
                case BhanuEvent.PipeNoLongerInTheSocket:
                    PipeNoLongerInTheSocketAction?.Invoke(); 
                return;
                
                case BhanuEvent.PipePickedUp:
                    PipePickedUpAction?.Invoke(); 
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
            }
        }

        #endregion

        #region Subscribe To Events
        
        public static void SubscribeToEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.AllElectricBoxesCollided:
                    AllElectricBoxesCollidedAction += actionFunction;
                return;
                
                case BhanuEvent.AllSocketsFilled:
                    AllSocketsFilledAction += actionFunction;
                return;
                
                case BhanuEvent.Death:
                    DeathAction += actionFunction;
                return;

                case BhanuEvent.ElectricBoxCollided:
                    ElectricBoxCollidedAction += actionFunction;
                return;
                
                case BhanuEvent.ElectricBoxNotCollided:
                    ElectricBoxNotCollidedAction += actionFunction;
                return;

                case BhanuEvent.PipeDropped:
                    PipeDroppedAction += actionFunction;
                return;
                
                case BhanuEvent.PipeInTheSocket:
                    PipeInTheSocketAction += actionFunction;
                return;
                
                case BhanuEvent.PipeNoLongerInTheSocket:
                    PipeNoLongerInTheSocketAction += actionFunction;
                return;
                
                case BhanuEvent.PipePickedUp:
                    PipePickedUpAction += actionFunction;
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
            }
        }

        #endregion

        #region Unsubscribe From Events
        
        public static void UnsubscribeFromEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.AllElectricBoxesCollided:
                    AllElectricBoxesCollidedAction -= actionFunction;
                return;
                
                case BhanuEvent.AllSocketsFilled:
                    AllSocketsFilledAction -= actionFunction;
                return;
                
                case BhanuEvent.Death:
                    DeathAction -= actionFunction;
                return;

                case BhanuEvent.ElectricBoxCollided:
                    ElectricBoxCollidedAction -= actionFunction;
                return;
                
                case BhanuEvent.ElectricBoxNotCollided:
                    ElectricBoxNotCollidedAction -= actionFunction;
                return;

                case BhanuEvent.PipeDropped:
                    PipeDroppedAction -= actionFunction;
                return;
                
                case BhanuEvent.PipeInTheSocket:
                    PipeInTheSocketAction -= actionFunction;
                return;
                
                case BhanuEvent.PipeNoLongerInTheSocket:
                    PipeNoLongerInTheSocketAction -= actionFunction;
                return;
                
                case BhanuEvent.PipePickedUp:
                    PipePickedUpAction -= actionFunction;
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
            }
        }

        #endregion
    }
}
