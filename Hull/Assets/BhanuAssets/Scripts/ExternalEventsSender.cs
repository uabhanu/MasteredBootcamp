using BhanuAssets.Scripts.Events;
using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class ExternalEventsSender : MonoBehaviour
    {
        [SerializeField] private int shipID;
        [SerializeField] private int playerID;

        //For simplicity, I am creating buttons for the respective external event
        
        public void PurchaseButton()
        {
            EventsManager.InvokeEvent(BhanuEvent.PurcaseShipEvent , playerID , shipID);
        }
    }
}
