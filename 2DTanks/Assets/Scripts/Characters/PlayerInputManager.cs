using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
    public class PlayerInputManager : MonoBehaviour
    {
        #region Private Variable Declarations
    
        private Vector2 _mouseMovement;
    
        #endregion
    
        #region Serialized Field Private Variable Declarations
    
        [SerializeField] private Camera mainCamera;

        #endregion
    
        #region Public Variable Declarations
    
        public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
        public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();
        public UnityEvent OnShoot = new UnityEvent();
    
        #endregion

        #region MonoBehaviour Functions

        private void Update()
        {
            GetBodyMovement();
            GetTurretMovement();
            GetShootingInput();
        }
    
        #endregion

        #region Custom Functions

        private Vector2 GetMousePosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane;
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            return mouseWorldPosition;
        }
    
        private void GetBodyMovement()
        {
            Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"));
            OnMoveBody?.Invoke(movementVector.normalized);
        }
    
        private void GetShootingInput()
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnShoot?.Invoke(); //If Event is not null, then Invoke
            }
        }

        private void GetTurretMovement()
        {
            OnMoveTurret?.Invoke(GetMousePosition());
        }

        #endregion
    }
}
