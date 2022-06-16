using Events;
using Interfaces;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour , IMoveBody , IMoveTurret , IShoot
{
    #region Private Variable Declarations
    
    private PlayerInputActions _playerInputActions;
    private Vector2 _mouseMovement;
    
    #endregion
    
    #region Serialized Field Private Variable Declarations
    
    [SerializeField] private Camera mainCamera;
    
    #endregion

    #region MonoBehaviour Functions
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Ground.Mouse.performed += x => _mouseMovement = x.ReadValue<Vector2>();
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
    
    // private Vector2 GetMousePosition()
    // {
    //     Vector3 mousePosition = _playerInputActions.Ground.Mouse.ReadValue<Vector2>();
    //     Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
    //     return mouseWorldPosition;
    //     //TODO This may not be returning the expected result so investigate
    // }

    private void MoveBodyInput()
    {
        if(_playerInputActions.Ground.Movement.triggered)
        {
            Vector2 movementVectorInput = _playerInputActions.Ground.Movement.ReadValue<Vector2>();
            EventsManager.InvokeEvent(PlayerInputEvent.InputEventMoveBody , movementVectorInput);
        }
        
        else if(_playerInputActions.Ground.Movement.WasReleasedThisFrame())
        {
            Vector2 movementVectorInput = Vector2.zero;
            EventsManager.InvokeEvent(PlayerInputEvent.InputEventMoveBody , movementVectorInput);
        }
    }

    private void MoveTurretInput()
    {
        if(_playerInputActions.Ground.Mouse.triggered)
        {
            EventsManager.InvokeEvent(PlayerInputEvent.InputEventMoveTurret , _mouseMovement);
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
