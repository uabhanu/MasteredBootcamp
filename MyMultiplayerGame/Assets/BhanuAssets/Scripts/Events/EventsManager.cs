using System;

namespace Events
{
    public class EventsManager
    {
        #region Event Actions Declarations
        private static event Action CreateRoomFailedAction;
        private static event Action CreateRoomRequestAction;
        private static event Action ConnectedToMasterAction;
        private static event Action JoinedLobbyAction;
        private static event Action JoinedRoomAction;
        private static event Action LeftRoomAction;
        #endregion

        #region Invoke Functions
        public static void InvokeEvent(BhanuEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case BhanuEvent.ConnectedToMasterEvent:
                    ConnectedToMasterAction?.Invoke(); 
                return;
                
                case BhanuEvent.CreateRoomFailedEvent:
                    CreateRoomFailedAction?.Invoke(); 
                return;
                
                case BhanuEvent.CreateRoomRequestEvent:
                    CreateRoomRequestAction?.Invoke(); 
                return;
                
                case BhanuEvent.JoinedLobbyEvent:
                    JoinedLobbyAction?.Invoke(); 
                return;
                
                case BhanuEvent.JoinedRoomEvent:
                    JoinedRoomAction?.Invoke(); 
                return;
                
                case BhanuEvent.LeftRoomEvent:
                    LeftRoomAction?.Invoke(); 
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
                
                case BhanuEvent.CreateRoomFailedEvent:
                    CreateRoomFailedAction += actionFunction;
                return;
                
                case BhanuEvent.CreateRoomRequestEvent:
                    CreateRoomRequestAction += actionFunction;
                return;
                
                case BhanuEvent.JoinedLobbyEvent:
                    JoinedLobbyAction += actionFunction;
                return;
                
                case BhanuEvent.JoinedRoomEvent:
                    JoinedRoomAction += actionFunction;
                return;
                
                case BhanuEvent.LeftRoomEvent:
                    LeftRoomAction += actionFunction;
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
                
                case BhanuEvent.CreateRoomFailedEvent:
                    CreateRoomFailedAction -= actionFunction;
                return;
                
                case BhanuEvent.CreateRoomRequestEvent:
                    CreateRoomRequestAction -= actionFunction;
                return;
                
                case BhanuEvent.JoinedLobbyEvent:
                    JoinedLobbyAction -= actionFunction;
                return;
                
                case BhanuEvent.JoinedRoomEvent:
                    JoinedRoomAction -= actionFunction;
                return;
                
                case BhanuEvent.LeftRoomEvent:
                    LeftRoomAction -= actionFunction;
                return;
            }
        }
        #endregion
    }
}
