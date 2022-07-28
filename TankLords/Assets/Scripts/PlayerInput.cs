using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private Camera mainCamera;
    
    public UnityEvent OnShoot;
    public UnityEvent<Vector2> OnMoveBody;
    public UnityEvent<Vector2> OnMoveTurret;
    
    #endregion
    
    #region Functions
    
    private void Update()
    {
        GetBodyMovement();
        GetShootingInput();
        GetTurretMovement();
    }

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
            OnShoot?.Invoke();
        }
    }

    private void GetTurretMovement()
    {
        OnMoveTurret?.Invoke(GetMousePosition());   
    }
    
    #endregion
}
