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
        private PhotonView _photonView;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private CapsuleCollider pipeCollider;
        [SerializeField] private float pipeDropDelay;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Transform socketTransform;

        [SerializeField] private int testVar = 0;

        #endregion

        #region MonoBehaviour & User Helper Functions
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if(_collidedPlayerObj != null && _isInPlayerHand)
            {
                transform.position = _collidedPlayerObj.GetComponent<Player>().RightHandTransform.position;   
            }

            else if(_collidedSocketObj != null && _isInTheSocket)
            {
                transform.position = _collidedSocketObj.transform.position;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Player") && !_isInTheSocket)
            {
                if(!_isInPlayerHand)
                {
                    _isInPlayerHand = true;
                    pipeCollider.isTrigger = true;
                }
                
                _collidedPlayerObj = collision.gameObject;
                GetComponent<MeshRenderer>().material = _collidedPlayerObj.GetComponent<SkinnedMeshRenderer>().material;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.tag.Equals("Socket"))
            {
                if(_isInPlayerHand)
                {
                    EventsManager.InvokeEvent(BhanuEvent.PipeInTheSocket);
                    _isInPlayerHand = false;
                }
                
                _collidedSocketObj = collider.gameObject;
                _collidedPlayerObj = null;
                _isInTheSocket = true;

                if(gameObject.activeSelf)
                {
                    StartCoroutine(DropPipe());   
                }
            }
        }
        
        private IEnumerator DropPipe()
        {
            yield return new WaitForSeconds(pipeDropDelay);
            EventsManager.InvokeEvent(BhanuEvent.PipeNoLongerInTheSocket);
            _collidedSocketObj = null;

            if(!_isInPlayerHand && _isInTheSocket)
            {
                _isInTheSocket = false;
                pipeCollider.isTrigger = false;
            }
        }
        
        public GameObject CollidedSocketObj
        {
            get => _collidedSocketObj;
            set => _collidedSocketObj = value;
        }

        #endregion
    }
}
