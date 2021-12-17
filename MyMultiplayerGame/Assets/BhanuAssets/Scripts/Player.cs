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

        private Animator _anim;
        private CinemachineVirtualCamera _cvm;
        private GameObject[] _electricalBoxes;
        private Material _materialToUse;
        private SkinnedMeshRenderer _playerRenderer;
        
        #endregion
        
        #region Serialized Private Variables Declarations
        
        [SerializeField] private PhotonView photonView;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private TMP_InputField nameInputTMP;
        [SerializeField] private TMP_Text nameTMP;

        #endregion

        #region MonoBehaviour Functions
        private void Start()
        {
            _anim = GetComponent<Animator>();
            _electricalBoxes = GameObject.FindGameObjectsWithTag("Electric");

            if(photonView.IsMine)
            {
                _cvm = GameObject.FindGameObjectWithTag("Follow").GetComponent<CinemachineVirtualCamera>();
                _cvm.Follow = transform;
                _cvm.LookAt = transform;
            }
            
            photonView.RPC("SelectRenderer" , RpcTarget.All);
        }

        private void Update()
        {
            photonView.RPC("Move" , RpcTarget.All);

            if(playerData.ElectricBoxesCollided == _electricalBoxes.Length)
            {
                EventsManager.InvokeEvent(BhanuEvent.WinEvent);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag.Equals("Electric"))
            {
                if(photonView.IsMine && photonView.AmController)
                {
                    EventsManager.InvokeEvent(BhanuEvent.ElectricBoxCollidedEvent);
                    photonView.RPC("ElectricBoxCollidedRPC" , RpcTarget.All);
                }
            }
        }
        
        #endregion
        
        #region User Functions
        
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
            if(photonView.IsMine)
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
        private void SelectRenderer()
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

        public void UpdateName()
        {
            nameTMP.enabled = true;
            nameTMP.text = nameInputTMP.text;
            nameInputTMP.enabled = false;
        }
        
        #endregion
    }
}
