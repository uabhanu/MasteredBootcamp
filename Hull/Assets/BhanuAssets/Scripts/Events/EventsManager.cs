using System;
using BhanuAssets.Scripts.Events;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        
        private static event Action<int , int> _purchaseShipAction;

        #endregion

        #region Invoke Functions
        
        public static void InvokeEvent(BhanuEvent eventToInvoke , int playerID , int shipID)
        {
            switch(eventToInvoke)
            {
                case BhanuEvent.PurcaseShipEvent:
                    _purchaseShipAction?.Invoke(playerID , shipID); 
                break;
            }
        }

        #endregion

        #region Subscribe To Events
        
        public static void SubscribeToEvent(BhanuEvent eventToSubscribe , Action<int , int> actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.PurcaseShipEvent:
                    _purchaseShipAction += actionFunction;
                break;
            }
        }

        #endregion

        #region Unsubscribe From Events
        
        public static void UnsubscribeFromEvent(BhanuEvent eventToSubscribe , Action<int , int> actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.PurcaseShipEvent:
                    _purchaseShipAction -= actionFunction;
                    break;
            }
        }

        #endregion
    }
}
