using System;

namespace Events
{
    public class EventsManager
    {
        private static event Action DoorCloseAction;
        private static event Action DoorOpenAction;
        private static event Action HealthGainAction;
        private static event Action HealthLossAction;

        public static void InvokeEvent(BhanuSkillsEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuSkillsEvent.DoorCloseEvent:
                    DoorCloseAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.DoorOpenEvent:
                    DoorOpenAction?.Invoke(); 
                return;
                
                case BhanuSkillsEvent.HealthLossEvent:
                    HealthGainAction?.Invoke(); 
                return;
            
                case BhanuSkillsEvent.HealthGainEvent:
                    HealthLossAction?.Invoke(); 
                return;
            }
        }

        public static void SubscribeToEvent(BhanuSkillsEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuSkillsEvent.DoorCloseEvent:
                    DoorCloseAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.DoorOpenEvent:
                    DoorOpenAction += actionFunction;
                return;
                
                case BhanuSkillsEvent.HealthLossEvent:
                    HealthGainAction += actionFunction;
                return;
            
                case BhanuSkillsEvent.HealthGainEvent:
                    HealthLossAction += actionFunction;
                return;
            }
        }

        public static void UnsubscribeFromEvent(BhanuSkillsEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuSkillsEvent.DoorCloseEvent:
                    DoorCloseAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.DoorOpenEvent:
                    DoorOpenAction -= actionFunction;
                return;
                
                case BhanuSkillsEvent.HealthLossEvent:
                    HealthGainAction -= actionFunction;
                return;
            
                case BhanuSkillsEvent.HealthGainEvent:
                    HealthLossAction -= actionFunction;
                return;
            }
        }
    }
}
