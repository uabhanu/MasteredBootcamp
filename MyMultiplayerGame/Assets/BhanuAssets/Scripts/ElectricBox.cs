using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class ElectricBox : MonoBehaviour
    {
        [SerializeField] private GameObject wallObj;
        [SerializeField] private Collider collider;

        #region Unity and other Functions
        
        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion
        
        #region Event Functions

        private void OnElectricBoxCollided()
        {
            collider.enabled = false;
            wallObj.SetActive(true);
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.ElectricBoxCollidedEvent , OnElectricBoxCollided);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.ElectricBoxCollidedEvent , OnElectricBoxCollided);
        }
        
        #endregion
    }
}
