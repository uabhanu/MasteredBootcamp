using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using System.Collections;
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
        private GameObject[] _pipes;
        private Material _defaultMaterial;
        
        #endregion

        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private CapsuleCollider pipeCollider;
        [SerializeField] private Material materialToUse;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Transform socketTransform;
        
        #endregion

        #region MonoBehaviour Functions
        
        private void Awake()
        {
            _defaultMaterial = GetComponent<MeshRenderer>().material;
            PipeSocketReset();
            _pipes = GameObject.FindGameObjectsWithTag("Pipe");
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
            
            if(playerData.PipesInTheSocket == _pipes.Length)
            {
                EventsManager.InvokeEvent(BhanuEvent.Win);
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
                PipeInTheSocket();
                StartCoroutine(DropPipe());
            }
        }
        
        private IEnumerator DropPipe()
        {
            yield return new WaitForSeconds(2f);
            _collidedSocketObj = null;
            GetComponent<MeshRenderer>().material = _defaultMaterial;
            _isInTheSocket = false;
            PipeNoLongerInTheSocket();
            pipeCollider.isTrigger = false;
        }
        
        #endregion

        #region User Functions

        private void PipeInTheSocket()
        {
            //LogMessages.AllIsWellMessage("RPC : Electric Box Collided");

            if(playerData.PipesInTheSocket < _pipes.Length)
            {
                playerData.PipesInTheSocket++;   
            }
        }
        
        private void PipeNoLongerInTheSocket()
        {
            //LogMessages.AllIsWellMessage("RPC : Electric Box No Longer Collided");

            if(playerData.PipesInTheSocket > 0)
            {
                playerData.PipesInTheSocket--;   
            }
        }

        private void PipeSocketReset()
        {
            playerData.PipesInTheSocket = 0;
        }

        #endregion
    }
}
