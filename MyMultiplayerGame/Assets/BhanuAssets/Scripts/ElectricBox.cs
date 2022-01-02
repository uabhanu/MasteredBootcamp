using Assets.BhanuAssets.Scripts.ScriptableObjects;
using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class ElectricBox : MonoBehaviour
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
            EventsManager.SubscribeToEvent(BhanuEvent.ElectricBoxCollided , OnElectricBoxCollided);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.ElectricBoxCollided , OnElectricBoxCollided);
        }
        
        #endregion
    }
}
