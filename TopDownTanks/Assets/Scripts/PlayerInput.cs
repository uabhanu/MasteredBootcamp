using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    #region Variable Declarations
    
    [SerializeField] private Camera mainCamera;
    
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();
    public UnityEvent OnShoot = new UnityEvent();
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        GetBodyMovement();
        GetTurrentMovement();
        GetShootInput();
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
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }

    private void GetTurrentMovement()
    {
        OnMoveTurret?.Invoke(GetMousePosition());
    }

    private void GetShootInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }
    
    #endregion
}
