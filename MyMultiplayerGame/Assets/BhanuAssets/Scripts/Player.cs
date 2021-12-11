using BhanuAssets.Scripts.ScriptableObjects;
using Cinemachine;
using Events;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        #region Private Variables Declarations
        
        private bool _readyToPlay;
        private CinemachineVirtualCamera _cvm;
        private GameObject[] _electricalBoxes;
        private Material _materialToUse;
        private MeshRenderer _playerRenderer;
        
        #endregion
        
        #region Serialized Private Variables Declarations

        [SerializeField] private PhotonView photonView;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private TMP_InputField nameInputTMP;
        [SerializeField] private TMP_Text nameTMP;
        
        #endregion

        #region Unity & Other Functions
        private void Start()
        {
            _electricalBoxes = GameObject.FindGameObjectsWithTag("Electric");
            playerData.ElectricBoxesCollided = 0;
            
            if(photonView.IsMine)
            {
                _cvm = GameObject.FindGameObjectWithTag("Follow").GetComponent<CinemachineVirtualCamera>();
                _cvm.Follow = transform;
                _cvm.LookAt = transform;
            }
            
            //nameInputTMP.enabled = true;
            _readyToPlay = true;
            photonView.RPC("SelectRenderer" , RpcTarget.All);
        }

        private void Update()
        {
            photonView.RPC("Move" , RpcTarget.All);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag.Equals("Electric"))
            {
                if(photonView.IsMine && photonView.AmController)
                {
                    photonView.RPC("ElectricBoxCollidedRPC" , RpcTarget.All);
                }

                if(playerData.ElectricBoxesCollided == _electricalBoxes.Length)
                {
                    EventsManager.InvokeEvent(BhanuEvent.WinEvent);
                }
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            if(other.gameObject.tag.Equals("Electric"))
            {
                if(photonView.IsMine && photonView.AmController)
                {
                    photonView.RPC("ElectricBoxNoLongerCollidedRPC" , RpcTarget.All);
                }
            }
        }

        public void UpdateName()
        {
            nameTMP.enabled = true;
            nameTMP.text = nameInputTMP.text;
            nameInputTMP.enabled = false;
        }
        
        #endregion
        
        #region RPC Functions

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
        private void Move()
        {
            if(photonView.IsMine/* && _readyToPlay*/)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                
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
        private void SelectRenderer()
        {
            _playerRenderer = GetComponent<MeshRenderer>();
            
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
        
        #endregion
    }
}
