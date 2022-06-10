using MyAssets.Scripts.Events;
using UnityEngine;

namespace MyAssets.Scripts
{
    public class ExternalEventsSender : MonoBehaviour
    {
        [SerializeField] private int shipID;

        //For simplicity, I am creating buttons for the respective external event
        
        public void PurchaseButton()
        {
            EventsManager.InvokeEvent(HullEvent.PurcaseShipEvent , shipID);
        }
    }
}
