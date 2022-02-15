using Bhanu;
using BhanuAssets.Scripts.ScriptableObjects;
using Cinemachine;
using Events;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        #region Private Variables Declarations

        private Animator _anim;
        private bool _isGrounded;
        private CinemachineVirtualCamera _cvm;
        private GameObject[] _cylinders;
        private Material _materialToUse;
        private PhotonView _photonView;
        private SkinnedMeshRenderer _playerRenderer;
        private Vector3 _playerVelocity;
        
        #endregion
        
        #region Serialized Private Variables Declarations

        [SerializeField] private bool isMultiplayerGame;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Rigidbody playerBody;
        [SerializeField] private TMP_Text nameTMP;
        [SerializeField] private Transform rightHandTransform;

        #endregion

        #region MonoBehaviour Functions

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _cylinders = GameObject.FindGameObjectsWithTag("Cylinder");

            if(isMultiplayerGame)
            {
                if(_photonView != null && _photonView.IsMine && _photonView.AmController)
                {
                    _cvm = GameObject.FindGameObjectWithTag("Follow").GetComponent<CinemachineVirtualCamera>();
                    _cvm.Follow = transform;
                    _cvm.LookAt = transform;
                    _photonView.RPC("CylinderCollisionResetRPC" , RpcTarget.All);
                    _photonView.RPC("SelectRendererRPC" , RpcTarget.All);
                    _photonView.RPC("UpdateNameRPC" , RpcTarget.All);
                }
            }
            else
            {
                _cvm = GameObject.FindGameObjectWithTag("Follow").GetComponent<CinemachineVirtualCamera>();
                _cvm.Follow = transform;
                _cvm.LookAt = transform;
            }

            SubscribeToEvents();
        }

        private void OnApplicationQuit()
        {
            if(_photonView.IsMine)
            {
                LogMessages.ErrorMessage("You Quit :(");
            }
            
            else if(!_photonView.IsMine)
            {
                LogMessages.WarningMessage("Client Player Quit :(");
            }
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            if(isMultiplayerGame)
            {
                if(_photonView != null && _photonView.IsMine && _photonView.AmController)
                {
                    _photonView.RPC("JumpRPC" , RpcTarget.All);
                    _photonView.RPC("MoveRPC" , RpcTarget.All);   
                }
            }
            else
            {
                Move();
            }

            if(playerData.CylindersCollided == _cylinders.Length)
            {
                EventsManager.InvokeEvent(BhanuEvent.BothCylindersCollided);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Cylinder"))
            {
                if(_photonView != null && _photonView.IsMine && _photonView.AmController)
                {
                    _photonView.RPC("CylinderCollidedRPC" , RpcTarget.All);
                }
            }
            
            if(collision.gameObject.tag.Equals("Floor"))
            {
                if(_photonView != null && _photonView.IsMine)
                {
                    _isGrounded = true;
                }
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Cylinder"))
            {
                if(_photonView != null && _photonView.IsMine && _photonView.AmController)
                {
                    _photonView.RPC("CylinderNoLongerCollidedRPC" , RpcTarget.All);
                }   
            }

            if(collision.gameObject.tag.Equals("Floor"))
            {
                if(_photonView != null && _photonView.IsMine)
                {
                    _isGrounded = false;
                }
            }
        }

        #endregion

        #region Non-Multiplayer Functions
        
        private void Move()
        {
            if(_isGrounded && _photonView.IsMine)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                
                _anim.SetFloat("MovementX" , horizontalInput);
                _anim.SetFloat("MovementZ" , verticalInput);
            
                Vector3 moveInCameraDirection = new Vector3(horizontalInput , 0f , verticalInput);
                moveInCameraDirection = moveInCameraDirection.x * _cvm.transform.right.normalized + moveInCameraDirection.z * _cvm.transform.forward.normalized;
                moveInCameraDirection.y = 0f;

                transform.Translate(moveInCameraDirection * playerData.MoveSpeed * Time.deltaTime , Space.World);
            
                if(horizontalInput == 0 && verticalInput == 0)
                {
                    moveInCameraDirection = Vector3.zero;
                }
            
                if(moveInCameraDirection != Vector3.zero)
                {
                    Quaternion rotationDirection = Quaternion.LookRotation(moveInCameraDirection , Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation , rotationDirection , playerData.RotationSpeed * Time.deltaTime);
                }
            }
        }

        #endregion
        
        #region User Functions

        [PunRPC]
        private void DestroyRPCs()
        {
            PhotonNetwork.DestroyAll();
        }

        [PunRPC]
        private void CylinderCollidedRPC()
        {
            //LogMessages.AllIsWellMessage("RPC : Electric Box Collided");

            if(playerData.CylindersCollided < _cylinders.Length)
            {
                playerData.CylindersCollided++;   
            }
        }
        
        [PunRPC]
        private void CylinderNoLongerCollidedRPC()
        {
            //LogMessages.AllIsWellMessage("RPC : Electric Box No Longer Collided");

            if(playerData.CylindersCollided > 0)
            {
                playerData.CylindersCollided--;   
            }
        }

        [PunRPC]
        private void CylinderCollisionResetRPC()
        {
            playerData.CylindersCollided = 0;
        }

        [PunRPC]
        private void JumpRPC()
        {
            if(_isGrounded && Input.GetKeyDown(KeyCode.Space) && _photonView.IsMine)
            {
                playerBody.AddForce(Vector3.up * playerData.JumpForce * Time.deltaTime , ForceMode.Impulse);
                _anim.SetTrigger("Jump");
            }
        }
        
        [PunRPC]
        private void MoveRPC()
        {
            if(_isGrounded && _photonView.IsMine)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                
                _anim.SetFloat("MovementX" , horizontalInput);
                _anim.SetFloat("MovementZ" , verticalInput);
            
                Vector3 moveInCameraDirection = new Vector3(horizontalInput , 0f , verticalInput);
                moveInCameraDirection = moveInCameraDirection.x * _cvm.transform.right.normalized + moveInCameraDirection.z * _cvm.transform.forward.normalized;
                moveInCameraDirection.y = 0f;

                transform.Translate(moveInCameraDirection * playerData.MoveSpeed * Time.deltaTime , Space.World);
            
                if(horizontalInput == 0 && verticalInput == 0)
                {
                    moveInCameraDirection = Vector3.zero;
                }
            
                if(moveInCameraDirection != Vector3.zero)
                {
                    Quaternion rotationDirection = Quaternion.LookRotation(moveInCameraDirection , Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation , rotationDirection , playerData.RotationSpeed * Time.deltaTime);
                }
            }
        }

        [PunRPC]
        private void SelectRendererRPC()
        {
            _playerRenderer = GetComponent<SkinnedMeshRenderer>();
    
            if(_photonView.IsMine)
            {
                _materialToUse = playerData.LocalMaterial;
                _playerRenderer.material = _materialToUse;
            }
            else
            {
                _materialToUse = playerData.RemoteMaterial;
                _playerRenderer.material = _materialToUse;
            }
        }
        
        [PunRPC]
        private void UpdateNameRPC()
        {
            nameTMP.text = _photonView.Controller.NickName;
        }
        
        public Transform RightHandTransform
        {
            get => rightHandTransform;
            set => rightHandTransform = value;
        }

        #endregion

        #region Event Functions

        private void OnDeath()
        {
            _photonView.RPC("DestroyRPCs" , RpcTarget.All);
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.Death , OnDeath);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.Death , OnDeath);
        }
        
        #endregion
    }
}
