using System;

namespace MyAssets.Scripts.Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        
        private static event Action<int> PurchaseShipAction;

        #endregion

        #region Invoke Functions
        
        public static void InvokeEvent(HullEvent eventToInvoke , int shipID)
        {
            switch(eventToInvoke)
            {
                case HullEvent.PurcaseShipEvent:
                    PurchaseShipAction?.Invoke(shipID); 
                break;
            }
        }

        #endregion

        #region Subscribe To Events
        
        public static void SubscribeToEvent(HullEvent eventToSubscribe , Action<int> actionFunction)
        {
            switch(eventToSubscribe)
            {
                case HullEvent.PurcaseShipEvent:
                    PurchaseShipAction += actionFunction;
                break;
            }
        }

        #endregion

        #region Unsubscribe From Events
        
        public static void UnsubscribeFromEvent(HullEvent eventToSubscribe , Action<int> actionFunction)
        {
            switch(eventToSubscribe)
            {
                case HullEvent.PurcaseShipEvent:
                    PurchaseShipAction -= actionFunction;
                break;
            }
        }

        #endregion
    }
}
