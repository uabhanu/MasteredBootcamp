using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using System.Collections;
using Bhanu;
using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Sword : MonoBehaviour
    {
        #region Private Variables Declarations

        private const float DEFAULTXROTATION = 270f;
        private const float INPLAYERHANDXROTATION = 0f;
        
        private bool _isInPlayerHand;
        private bool _isInTheSocket;
        private GameObject _collidedPlayerObj;
        private GameObject _collidedSocketObj;
        private Material _mechanicalEyeMaterial;
        private PhotonView _photonView;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private Collider swordCollider;
        [SerializeField] private float swordDropDelay;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Rigidbody swordBody;
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

            // else if(_collidedSocketObj != null && _isInTheSocket)
            // {
            //     transform.position = _collidedSocketObj.transform.position;
            // }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Player") && !_isInTheSocket)
            {
                if(!_isInPlayerHand)
                {
                    _isInPlayerHand = true;
                    swordCollider.isTrigger = true;
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
                LogMessages.AllIsWellMessage("Sword Collided with Mechanic Eye");
                _mechanicalEyeMaterial = playerData.MechanicalEyeMaterialGreen;
                swordBody.constraints = RigidbodyConstraints.FreezePositionY;

                if(_isInPlayerHand)
                {
                    EventsManager.InvokeEvent(BhanuEvent.SwordInTheSocket);
                    _isInPlayerHand = false;
                    Vector3 rotationVector = new Vector3(DEFAULTXROTATION , 0f , 0f);
                    Quaternion rotation = Quaternion.Euler(rotationVector);
                    transform.rotation = rotation;
                }
                
                _collidedSocketObj = collider.gameObject;
                _collidedSocketObj.GetComponentInParent<MeshRenderer>().material = _mechanicalEyeMaterial;
                transform.position = _collidedSocketObj.transform.position;
                _collidedPlayerObj = null;
                _isInTheSocket = true;

                if(gameObject.activeSelf)
                {
                    StartCoroutine(DropSword());   
                }
            }
        }
        
        private IEnumerator DropSword()
        {
            yield return new WaitForSeconds(swordDropDelay);
            EventsManager.InvokeEvent(BhanuEvent.SwordNoLongerInTheSocket);
            _mechanicalEyeMaterial = playerData.MechanicalEyeMaterialRed;
            _collidedSocketObj.GetComponentInParent<MeshRenderer>().material = _mechanicalEyeMaterial;
            _collidedSocketObj = null;

            if(!_isInPlayerHand && _isInTheSocket)
            {
                _isInTheSocket = false;
                swordCollider.isTrigger = false;
                swordBody.constraints = RigidbodyConstraints.FreezeRotation;
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
