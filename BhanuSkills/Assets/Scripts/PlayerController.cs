using Cinemachine;
using ScriptableObjects;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const float _threshold = 0.01f;
	
	private bool _lockCameraPosition = false;
	private bool _rotateOnMove = true;
    private CharacterController _characterController;
    private float _animationBlend;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float _currentSpeed;
    private float _rotationVelocity;
    private float _sensitivity = 1f;
    private float _targetRotation = 0f;
    private StarterAssetsInputs _starterAssetsInputs;

    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera cmAimCamera;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private float cameraAngleOverride = 0.0f;
    [SerializeField] private float clampBottom = -30f;
    [SerializeField] private float clampTop = 70f;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private GameObject cinemachineCameraTarget;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform debugTransform;

    private void Awake()
    {
	    _currentSpeed = playerData.WalkSpeed;
	    _characterController = GetComponent<CharacterController>();
	    playerData.AssignAnimationIDs();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
	    Move();
	    
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 centerOfTheScreen = new Vector2(Screen.width / 2 , Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(centerOfTheScreen);

        if(Physics.Raycast(ray , out RaycastHit rayHit , 999f , aimLayerMask))
        {
            //debugTransform.position = rayHit.point; //This is for testing only
            mouseWorldPosition = rayHit.point;
        }
        
        if(_starterAssetsInputs.aim)
        {
            cmAimCamera.gameObject.SetActive(true);
            SetRotateOnMove(false);
            SetSensitivity(aimSensitivity);
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward , aimDirection , Time.deltaTime * 20f);
        }
        else
        {
            cmAimCamera.gameObject.SetActive(false);
            SetRotateOnMove(true);
            SetSensitivity(normalSensitivity);
        }
    }
    
    private void LateUpdate()
    {
        CameraRotation();
    }
    
    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if(_starterAssetsInputs.look.sqrMagnitude >= _threshold && !_lockCameraPosition)
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
    
    private void Move()
	{
		// set target speed based on move speed, sprint speed and if sprint is pressed
		float targetSpeed = _starterAssetsInputs.sprint ? playerData.RunSpeed : playerData.WalkSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if(_starterAssetsInputs.move == Vector2.zero) targetSpeed = 0.0f;

		// a reference to the players current horizontal velocity
		float currentHorizontalSpeed = new Vector3(_characterController.velocity.x , 0.0f , _characterController.velocity.z).magnitude;
		float inputMagnitude = _starterAssetsInputs.analogMovement ? _starterAssetsInputs.move.magnitude : 1f;
		float speedOffset = 1.1f;

		// accelerate or decelerate to target speed
		if(currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			_currentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * playerData.SpeedChangeRate);

			// round speed to 3 decimal places
			_currentSpeed = Mathf.Round(_currentSpeed * 1000f) / 1000f;
		}
		else
		{
			_currentSpeed = targetSpeed;
		}
		
		_animationBlend = Mathf.Lerp(_animationBlend , targetSpeed , Time.deltaTime * playerData.SpeedChangeRate);

		// normalise input direction
		Vector3 inputDirection = new Vector3(_starterAssetsInputs.move.x , 0.0f , _starterAssetsInputs.move.y).normalized;

		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		if(_starterAssetsInputs.move != Vector2.zero)
		{
			_targetRotation = Mathf.Atan2(inputDirection.x , inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y , _targetRotation , ref _rotationVelocity , playerData.RotationSmoothTime);

			if(_rotateOnMove)
			{
				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0.0f , rotation , 0.0f);	
			}
		}


		Vector3 targetDirection = Quaternion.Euler(0.0f , _targetRotation , 0.0f) * Vector3.forward;

		// move the player
		_characterController.Move(targetDirection.normalized * (_currentSpeed * Time.deltaTime) + new Vector3(0.0f , playerData.VerticalVelocity , 0.0f) * Time.deltaTime);
		
		animator.SetFloat(playerData.AnimIDSpeed , _animationBlend);
		animator.SetFloat(playerData.AnimIDMotionSpeed , inputMagnitude);
		
	}
    
    private void SetRotateOnMove(bool newRotateOnMove)
    {
	    _rotateOnMove = newRotateOnMove;
    }
    
    private void SetSensitivity(float newSensitivity)
    {
        _sensitivity = newSensitivity;
    }
}
