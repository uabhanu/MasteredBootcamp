using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private UnityEvent<Vector2> onMoveBody = new UnityEvent<Vector2>();
    [SerializeField] private UnityEvent<Vector2> onMoveTurret = new UnityEvent<Vector2>();
    [SerializeField] private UnityEvent onShoot = new UnityEvent();
    
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
        onMoveBody?.Invoke(movementVector.normalized);
    }

    private void GetTurrentMovement()
    {
        onMoveTurret?.Invoke(GetMousePosition());
    }

    private void GetShootInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            onShoot?.Invoke();
        }
    }
    
    #endregion
}
