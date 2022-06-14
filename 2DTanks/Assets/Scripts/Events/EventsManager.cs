using System;
using UnityEngine;

namespace Events
{
    public class EventsManager
    {
        #region Player Input Event Actions Declarations
        
        private static event Action<Vector2> MoveBodyAction;
        private static event Action<Vector2> MoveTurretAction;
        private static event Action ShootAction;

        #endregion

        #region Player Input Invoke Functions
        
        public static void InvokeEvent(PlayerInputEvent eventToInvoke , Vector2 moveDirection)
        {
            switch(eventToInvoke)
            {
                case PlayerInputEvent.InputEventMoveBody:
                    MoveBodyAction?.Invoke(moveDirection);
                break;

                case PlayerInputEvent.InputEventMoveTurret:
                    MoveTurretAction?.Invoke(moveDirection);
                break;
            }
        }
        
        public static void InvokeEvent(PlayerInputEvent eventToInvoke)
        {
            switch(eventToInvoke)
            {
                case PlayerInputEvent.InputEventShoot:
                    ShootAction?.Invoke();
                break;
            }
        }

        #endregion

        #region Subscribe To Player Input Events
        
        public static void SubscribeToPlayerInputEvent(PlayerInputEvent eventToSubscribe , Action<Vector2> actionFunction)
        {
            switch(eventToSubscribe)
            {
                case PlayerInputEvent.InputEventMoveBody:
                    MoveBodyAction += actionFunction;
                break;
                
                case PlayerInputEvent.InputEventMoveTurret:
                    MoveTurretAction += actionFunction;
                break;
            }
        }
        
        public static void SubscribeToPlayerInputEvent(PlayerInputEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case PlayerInputEvent.InputEventShoot:
                    ShootAction += actionFunction;
                break;
            }
        }

        #endregion

        #region Unsubscribe From Player Input Events
        
        public static void UnsubscribeFromPlayerInputEvent(PlayerInputEvent eventToSubscribe , Action<Vector2> actionFunction)
        {
            switch(eventToSubscribe)
            {
                case PlayerInputEvent.InputEventMoveBody:
                    MoveBodyAction -= actionFunction;
                break;
                
                case PlayerInputEvent.InputEventMoveTurret:
                    MoveTurretAction -= actionFunction;
                break;
            }
        }
        
        public static void UnsubscribeFromPlayerInputEvent(PlayerInputEvent eventToSubscribe , Action actionFunction)
        {
            switch(eventToSubscribe)
            {
                case PlayerInputEvent.InputEventShoot:
                    ShootAction -= actionFunction;
                break;
            }
        }

        #endregion
    }
}
