using System;
using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using System.Collections;
using Bhanu;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Pipe : MonoBehaviour
    {
        #region Private Variables Declarations

        private const float DEFAULTXROTATION = 270f;
        private const float INPLAYERHANDXROTATION = 0f;
        
        private bool _isInPlayerHand;
        private bool _isInTheSocket;
        private GameObject _collidedPlayerObj;
        private GameObject _collidedSocketObj;
        private PhotonView _photonView;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private Collider pipeCollider;
        [SerializeField] private float pipeDropDelay;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Transform socketTransform;

        #endregion

        #region MonoBehaviour & User Helper Functions
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            Vector3 rotationVector = new Vector3(DEFAULTXROTATION , 0f , 0f);
            Quaternion rotation = Quaternion.Euler(rotationVector);
            transform.rotation = rotation;
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
                    Vector3 rotationVector = new Vector3(INPLAYERHANDXROTATION , 0f , 0f);
                    Quaternion rotation = Quaternion.Euler(rotationVector);
                    transform.rotation = rotation;
                }
                
                _collidedPlayerObj = collision.gameObject;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.tag.Equals("Socket"))
            {
                LogMessages.AllIsWellMessage("Pipe Collided with Mechanic Eye");
                
                if(_isInPlayerHand)
                {
                    EventsManager.InvokeEvent(BhanuEvent.PipeInTheSocket);
                    _isInPlayerHand = false;
                    Vector3 rotationVector = new Vector3(DEFAULTXROTATION , 0f , 0f);
                    Quaternion rotation = Quaternion.Euler(rotationVector);
                    transform.rotation = rotation;
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
