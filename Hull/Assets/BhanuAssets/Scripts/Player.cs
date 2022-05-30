using BhanuAssets.Scripts.Events;
using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        private float _t;
        private Vector3 _startPosition;

        [SerializeField] private Animator animator;
        [SerializeField] private float timeOfTravel;
        [SerializeField] private int playerID;
        [SerializeField] private int shipID;
        [SerializeField] private Transform targetTransform;

        private void Start()
        {
            _startPosition = transform.position;
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            _t += Time.deltaTime / timeOfTravel;
            transform.position = Vector3.Lerp(_startPosition , targetTransform.position , _t);

            if(transform.position.y < 5f)
            {
                animator.SetBool("ReadyToRotate" , true);
            }
        }

        private void OnPurchaseShip(int pID , int sID)
        {
            // Node Js will send an external event for player to purchase a ship and once purchased, the player ID & shipID are assigned with pID and sID
            // that are the IDs given when player purchased the ship and ready to play.
            playerID = pID;
            shipID = sID;
            //Send this back to the server to save with NodeJs.
        }

        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.PurcaseShipEvent , OnPurchaseShip);
        }

        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.PurcaseShipEvent , OnPurchaseShip);
        }
    }
}
