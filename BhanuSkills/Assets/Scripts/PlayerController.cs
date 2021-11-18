using Cinemachine;
using ScriptableObjects;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _lockCameraPosition = false;
    
    private const float _threshold = 0.01f;
    
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float _sensitivity = 1f;
    private StarterAssetsInputs _starterAssetsInputs;

    [SerializeField] private CinemachineVirtualCamera cmAimCamera;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private float cameraAngleOverride = 0.0f;
    [SerializeField] private float clampBottom = -30f;
    [SerializeField] private float clampTop = 70f;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private GameObject cinemachineCameraTarget;
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform debugTransform;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if(_starterAssetsInputs.aim)
        {
            cmAimCamera.gameObject.SetActive(true);
            SetSensitiity(aimSensitivity);
        }
        else
        {
            cmAimCamera.gameObject.SetActive(false);
            SetSensitiity(normalSensitivity);
        }

        Vector2 centerOfTheScreen = new Vector2(Screen.width / 2 , Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(centerOfTheScreen);

        if(Physics.Raycast(ray , out RaycastHit rayHit , 999f , aimLayerMask))
        {
            debugTransform.position = rayHit.point;
        }
    }
    
    private void LateUpdate()
    {
        CameraRotation();
    }
    
    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_starterAssetsInputs.look.sqrMagnitude >= _threshold && !_lockCameraPosition)
        {
            _cinemachineTargetYaw += _starterAssetsInputs.look.x * Time.deltaTime * _sensitivity;
            _cinemachineTargetPitch += _starterAssetsInputs.look.y * Time.deltaTime * _sensitivity;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw , float.MinValue , float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch , clampBottom , clampTop);

        // Cinemachine will follow this target
        cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + cameraAngleOverride , _cinemachineTargetYaw , 0.0f);
    }
    
    private static float ClampAngle(float lfAngle , float lfMin , float lfMax)
    {
        if(lfAngle < -360f) lfAngle += 360f;
        if(lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle , lfMin , lfMax);
    }
    
    private void SetSensitiity(float newSensitivity)
    {
        _sensitivity = newSensitivity;
    }
}
