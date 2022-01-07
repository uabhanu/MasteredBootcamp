using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Pipe : MonoBehaviour
    {
        #region Private Variables Declarations
        
        private bool _isInPlayerHand;
        private bool _isInTheSocket;
        private GameObject _collidedPlayerObj;
        private GameObject _collidedSocketObj;
        private Material _defaultMaterial;
        private PhotonView _photonView;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private CapsuleCollider pipeCollider;
        [SerializeField] private float pipeDropDelay;
        [SerializeField] private Material materialToUse;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Transform socketTransform;

        #endregion

        #region MonoBehaviour & User Helper Functions
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            _defaultMaterial = GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            if(_collidedPlayerObj != null && _isInPlayerHand)
            {
                transform.position = _collidedPlayerObj.GetComponent<Player>().RightHandTransform.position;   
            }

            if(_collidedSocketObj != null && _isInTheSocket)
            {
                transform.position = _collidedSocketObj.transform.position;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Player"))
            {
                _collidedPlayerObj = collision.gameObject;
                GetComponent<MeshRenderer>().material = _collidedPlayerObj.GetComponent<SkinnedMeshRenderer>().material;
                _isInPlayerHand = true;
                pipeCollider.isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.tag.Equals("Socket"))
            {
                _collidedSocketObj = collider.gameObject;
                _collidedPlayerObj = null;
                GetComponent<MeshRenderer>().material = materialToUse;
                _isInPlayerHand = false;
                _isInTheSocket = true;

                if(_photonView != null && _photonView.IsMine && _photonView.AmController)
                {
                    EventsManager.InvokeEvent(BhanuEvent.PipeInTheSocket);
                }
                
                StartCoroutine(DropPipe());
            }
        }
        
        private IEnumerator DropPipe()
        {
            Socket socket = GameObject.FindGameObjectWithTag("Socket").GetComponent<Socket>();

            if(socket != null && !socket.AllPipesInTheSocket())
            {
                yield return new WaitForSeconds(pipeDropDelay);
                _collidedSocketObj = null;
                GetComponent<MeshRenderer>().material = _defaultMaterial;
                _isInTheSocket = false;
            
                if(_photonView != null && _photonView.IsMine && _photonView.AmController)
                {
                    EventsManager.InvokeEvent(BhanuEvent.PipeNoLongerInTheSocket);
                }

                pipeCollider.isTrigger = false;
            }
        }
        
        public bool IsInTheSocket
        {
            get => _isInTheSocket;
            set => _isInTheSocket = value;
        }
        
        #endregion
    }
}
