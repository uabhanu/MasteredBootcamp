using System;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        private static event Action ConnectedToMasterAction;
        private static event Action JoinedLobbyAction;
        #endregion

        #region Invoke Functions
        public static void InvokeEvent(BhanuEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuEvent.ConnectedToMasterEvent:
                    ConnectedToMasterAction?.Invoke(); 
                return;
                
                case BhanuEvent.JoinedLobbyEvent:
                    JoinedLobbyAction?.Invoke(); 
                return;
            }
        }
        #endregion

        #region Subscribe To Events
        public static void SubscribeToEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.ConnectedToMasterEvent:
                    ConnectedToMasterAction += actionFunction;
                return;
                
                case BhanuEvent.JoinedLobbyEvent:
                    JoinedLobbyAction += actionFunction;
                return;
            }
        }
        #endregion

        #region Unsubscribe From Events
        public static void UnsubscribeFromEvent(BhanuEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case BhanuEvent.ConnectedToMasterEvent:
                    ConnectedToMasterAction -= actionFunction;
                return;
                
                case BhanuEvent.JoinedLobbyEvent:
                    JoinedLobbyAction -= actionFunction;
                return;
            }
        }
        #endregion
    }
}
