using MyAssets.Scripts.Events;
using UnityEngine;

namespace MyAssets.Scripts
{
    public class ShipsManager : MonoBehaviour
    {
        private int _shipID = 0;
        
        [SerializeField] private GameObject shipPrefab;

        private void Start()
        {
            SubscribeToEvents();
        }
    
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void OnPurchaseShip(int shipID)
        {
            _shipID = shipID;
            Instantiate(shipPrefab , shipPrefab.transform.position , Quaternion.identity);
            shipPrefab.gameObject.GetComponent<Player>().ShipID = _shipID;
        }
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(HullEvent.PurcaseShipEvent , OnPurchaseShip);
        }

        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(HullEvent.PurcaseShipEvent , OnPurchaseShip);
        }
    }
}
