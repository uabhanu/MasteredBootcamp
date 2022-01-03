using System;
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
        private GameObject[] _electricalBoxes;
        private Material _materialToUse;
        private SkinnedMeshRenderer _playerRenderer;
        private Vector3 _playerVelocity;
        
        #endregion
        
        #region Serialized Private Variables Declarations
        
        [SerializeField] private PhotonView photonView;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Rigidbody playerBody;
        [SerializeField] private TMP_Text nameTMP;

        #endregion

        #region MonoBehaviour Functions

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _electricalBoxes = GameObject.FindGameObjectsWithTag("Electric");
            photonView.RPC("ElectricBoxCollisionReset" , RpcTarget.All);
            photonView.RPC("UpdateNameRPC" , RpcTarget.All);

            if(photonView.IsMine)
            {
                _cvm = GameObject.FindGameObjectWithTag("Follow").GetComponent<CinemachineVirtualCamera>();
                _cvm.Follow = transform;
                _cvm.LookAt = transform;
            }
            
            photonView.RPC("SelectRendererRPC" , RpcTarget.All);

            SubscribeToEvents();
        }

        private void OnApplicationQuit()
        {
            if(photonView.IsMine)
            {
                LogMessages.ErrorMessage("You Quit :(");
            }
            
            else if(!photonView.IsMine)
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
            photonView.RPC("JumpRPC" , RpcTarget.All);
            photonView.RPC("MoveRPC" , RpcTarget.All);

            if(playerData.ElectricBoxesCollided == _electricalBoxes.Length)
            {
                EventsManager.InvokeEvent(BhanuEvent.Win);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag.Equals("Death"))
            {
                EventsManager.InvokeEvent(BhanuEvent.Death);
            }

            if(other.gameObject.tag.Equals("Electric"))
            {
                if(photonView.IsMine && photonView.AmController)
                {
                    photonView.RPC("ElectricBoxCollidedRPC" , RpcTarget.All);
                }
            }
            
            if(other.gameObject.tag.Equals("Floor"))
            {
                if(photonView.IsMine)
                {
                    _isGrounded = true;
                }
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            if(photonView.IsMine && photonView.AmController)
            {
                photonView.RPC("ElectricBoxNoLongerCollidedRPC" , RpcTarget.All);
            }

            if(other.gameObject.tag.Equals("Floor"))
            {
                if(photonView.IsMine && photonView.AmController)
                {
                    _isGrounded = false;
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
        private void ElectricBoxCollidedRPC()
        {
            //LogMessages.AllIsWellMessage("RPC : Electric Box Collided");

            if(playerData.ElectricBoxesCollided < _electricalBoxes.Length)
            {
                playerData.ElectricBoxesCollided++;   
            }
        }
        
        [PunRPC]
        private void ElectricBoxNoLongerCollidedRPC()
        {
            //LogMessages.AllIsWellMessage("RPC : Electric Box No Longer Collided");

            if(playerData.ElectricBoxesCollided > 0)
            {
                playerData.ElectricBoxesCollided--;   
            }
        }

        [PunRPC]
        private void ElectricBoxCollisionReset()
        {
            playerData.ElectricBoxesCollided = 0;
        }

        [PunRPC]
        private void JumpRPC()
        {
            if(_isGrounded && Input.GetKeyDown(KeyCode.Space) && photonView.AmController && photonView.IsMine)
            {
                playerBody.AddForce(Vector3.up * playerData.JumpForce * Time.deltaTime , ForceMode.Impulse);
                _anim.SetTrigger("Jump");
            }
        }
        
        [PunRPC]
        private void MoveRPC()
        {
            nameTMP.transform.rotation = Quaternion.identity;
            
            if(_isGrounded && photonView.AmController && photonView.IsMine)
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
    
            if(photonView.IsMine)
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
            nameTMP.text = photonView.Controller.NickName;
        }

        #endregion

        #region Event Functions

        private void OnDeath()
        {
            photonView.RPC("DestroyRPCs" , RpcTarget.All);
            //PhotonNetwork.IsMessageQueueRunning = false;
            //PhotonNetwork.OpRemoveCompleteCache();
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
