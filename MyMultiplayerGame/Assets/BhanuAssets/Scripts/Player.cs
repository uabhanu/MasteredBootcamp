using BhanuAssets.Scripts.ScriptableObjects;
using Cinemachine;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        private bool _readyToPlay;
        private CinemachineVirtualCamera _cvm;
        private Material _materialToUse;
        private MeshRenderer _playerRenderer;

        [SerializeField] private PhotonView photonView;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private TMP_InputField nameInputTMP;
        [SerializeField] private TMP_Text nameTMP;

        private void Start()
        {
            _cvm = GameObject.FindGameObjectWithTag("Follow").GetComponent<CinemachineVirtualCamera>();
            _cvm.Follow = transform;
            _cvm.LookAt = transform;

            if(!photonView.IsMine)
            {
                _cvm = null;
            }
            
            //nameInputTMP.enabled = true;
            _readyToPlay = true;
            photonView.RPC("SelectRenderer" , RpcTarget.All);
        }

        private void Update()
        {
            photonView.RPC("Move" , RpcTarget.All);
        }
        
        [PunRPC]
        private void Move()
        {
            if(photonView.IsMine && _readyToPlay)
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

        public void UpdateName()
        {
            nameTMP.enabled = true;
            nameTMP.text = nameInputTMP.text;
            nameInputTMP.enabled = false;
        }
    }
}
