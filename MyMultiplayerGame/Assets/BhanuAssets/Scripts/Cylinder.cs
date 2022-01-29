using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Cylinder : MonoBehaviour
    {
        private bool playerCollided = false;

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
            playerCollided = true;
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.CylinderCollided , OnElectricBoxCollided);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.CylinderCollided , OnElectricBoxCollided);
        }
        
        #endregion
    }
}
