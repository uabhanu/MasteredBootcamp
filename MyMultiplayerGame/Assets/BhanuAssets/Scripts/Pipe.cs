using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Pipe : MonoBehaviour
    {
        private bool _isInPlayerHand;
        private bool _isInTheSocket;
        private GameObject _collidedPlayerObj;
        private GameObject _collidedSocketObj;
        private Material _defaultMaterial;

        [SerializeField] private CapsuleCollider pipeCollider;
        [SerializeField] private Material materialToUse;
        [SerializeField] private PhotonView photonView;
        [SerializeField] private Transform socketTransform;

        private void Awake()
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
                _isInPlayerHand = false;
                _isInTheSocket = true;
                GetComponent<MeshRenderer>().material = materialToUse;
                StartCoroutine(DropPipe());
            }
        }

        private IEnumerator DropPipe()
        {
            yield return new WaitForSeconds(2f);
            _collidedSocketObj = null;
            _isInTheSocket = false;
            pipeCollider.isTrigger = false;
            GetComponent<MeshRenderer>().material = _defaultMaterial;
        }
    }
}
