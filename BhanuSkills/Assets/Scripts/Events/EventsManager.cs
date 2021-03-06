using System;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        private static event Action AllCollectedAction;
        private static event Action CollectibleCollectedAction;
        private static event Action DiegeticUIEnabledAction;
        private static event Action DoorCloseAction;
        private static event Action DoorOpenAction;
        private static event Action GameOverAction;
        private static event Action HealthAlmostEmptyAction;
        private static event Action HealthGainAction;
        private static event Action HealthLossAction;
        private static event Action InfoAction;
        private static event Action KeyCollectedAction;
        private static event Action NonDiegeticUIEnabledAction;
        private static event Action PauseAction;
        private static event Action TrophyCollectedAction;
        private static event Action UnpauseAction;
        #endregion

        #region Invoke Functions
        public static void InvokeEvent(BhanuSkillsEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuSkillsEvent.AllCollectedEvent:
                    AllCollectedAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.CollectibleCollectedEvent:
                    CollectibleCollectedAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.DiegeticUIEnabledEvent:
                    DiegeticUIEnabledAction?.Invoke(); 
                return;

                case BhanuSkillsEvent.DoorCloseEvent:
                    DoorCloseAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.DoorOpenEvent:
                    DoorOpenAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.GameOverEvent:
                    GameOverAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.HealthAlmostEmptyEvent:
                    HealthAlmostEmptyAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.HealthGainEvent:
                    HealthLossAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.HealthLossEvent:
                    HealthGainAction?.Invoke(); 
                return;

                case BhanuSkillsEvent.InfoEvent:
                    InfoAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.KeyCollectedEvent:
                    KeyCollectedAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.NonDiegeticUIEnabledEvent:
                    NonDiegeticUIEnabledAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.PauseEvent:
                    PauseAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.TrophyCollectedEvent:
                    TrophyCollectedAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.UnpauseEvent:
                    UnpauseAction?.Invoke(); 
                return;
            }
        }
        #endregion

        #region Subscribe To Events
        public static void SubscribeToEvent(BhanuSkillsEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuSkillsEvent.AllCollectedEvent:
                    AllCollectedAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.CollectibleCollectedEvent:
                    CollectibleCollectedAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.DoorCloseEvent:
                    DoorCloseAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.DoorOpenEvent:
                    DoorOpenAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.DiegeticUIEnabledEvent:
                    DiegeticUIEnabledAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.GameOverEvent:
                    GameOverAction += actionFunction; 
                return;
                
                case BhanuSkillsEvent.HealthAlmostEmptyEvent:
                    HealthAlmostEmptyAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.HealthGainEvent:
                    HealthLossAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.HealthLossEvent:
                    HealthGainAction += actionFunction;
                return;

                case BhanuSkillsEvent.InfoEvent:
                    InfoAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.KeyCollectedEvent:
                    KeyCollectedAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.NonDiegeticUIEnabledEvent:
                    NonDiegeticUIEnabledAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.PauseEvent:
                    PauseAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.TrophyCollectedEvent:
                    TrophyCollectedAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.UnpauseEvent:
                    UnpauseAction += actionFunction;
                return;
            }
        }
        #endregion

        #region Unsubscribe From Events
        public static void UnsubscribeFromEvent(BhanuSkillsEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuSkillsEvent.AllCollectedEvent:
                    AllCollectedAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.CollectibleCollectedEvent:
                    CollectibleCollectedAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.DiegeticUIEnabledEvent:
                    DiegeticUIEnabledAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.DoorCloseEvent:
                    DoorCloseAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.DoorOpenEvent:
                    DoorOpenAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.GameOverEvent:
                    GameOverAction -= actionFunction; 
                return;
                
                case BhanuSkillsEvent.HealthAlmostEmptyEvent:
                    HealthAlmostEmptyAction -= actionFunction; 
                return;
                
                case BhanuSkillsEvent.HealthLossEvent:
                    HealthGainAction -= actionFunction;
                return;
            
                case BhanuSkillsEvent.HealthGainEvent:
                    HealthLossAction -= actionFunction;
                return;

                case BhanuSkillsEvent.InfoEvent:
                    InfoAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.KeyCollectedEvent:
                    KeyCollectedAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.NonDiegeticUIEnabledEvent:
                    NonDiegeticUIEnabledAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.PauseEvent:
                    PauseAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.TrophyCollectedEvent:
                    TrophyCollectedAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.UnpauseEvent:
                    UnpauseAction -= actionFunction;
                return;
            }
        }
        #endregion
    }
}
