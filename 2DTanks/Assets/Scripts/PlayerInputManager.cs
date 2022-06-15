using Events;
using Interfaces;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour , IMoveBody , IMoveTurret , IShoot
{
    #region Private Variable Declarations
    
    private PlayerInputActions _playerInputActions;
    
    #endregion
    
    #region Serialized Field Private Variable Declarations
    
    [SerializeField] private Camera mainCamera;
    
    #endregion

    #region MonoBehaviour Functions
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Update()
    {
        MoveBody();
        MoveTurret();
        Shoot();
    }
    
    #endregion
    
    #region Local Functions
    
    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = _playerInputActions.Ground.Movement.ReadValue<Vector2>();
        mousePosition.z += mainCamera.nearClipPlane;
        
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    private void MoveBodyInput()
    {
        Vector2 movementVectorInput = _playerInputActions.Ground.Movement.ReadValue<Vector2>();

        if(_playerInputActions.Ground.Movement.triggered)
        {
            EventsManager.InvokeEvent(PlayerInputEvent.InputEventMoveBody , movementVectorInput);
        }
    }

    private void MoveTurretInput()
    {
        if(_playerInputActions.Ground.Mouse.triggered)
        {
            EventsManager.InvokeEvent(PlayerInputEvent.InputEventMoveTurret , GetMousePosition());    
        }
    }

    private void ShootInput()
    {
        _playerInputActions.Ground.Shoot.ReadValue<float>();

        if(_playerInputActions.Ground.Shoot.triggered)
        {
            EventsManager.InvokeEvent(PlayerInputEvent.InputEventShoot);
        }
    }
    
    #endregion

    #region Interface Functions
    
    public void MoveBody()
    {
        MoveBodyInput();
    }

    public void MoveTurret()
    {
        MoveTurretInput();
    }
    
    public void Shoot()
    {
        ShootInput();
    }
    
    #endregion
}
