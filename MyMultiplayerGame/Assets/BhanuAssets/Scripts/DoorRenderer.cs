using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class DoorRenderer : MonoBehaviour
    {
        private bool _doorLocked = true;
        private Material _materialToUse;
        private MeshRenderer _doorRenderer;

        [SerializeField] private DoorData doorData; 

        #region Unity and other Functions
        private void Start()
        {
            SubscribeToEvents();
            UpdateRendering();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void UpdateRendering()
        {
            _doorRenderer = GetComponent<MeshRenderer>();
            
            if(_doorLocked)
            {
                _materialToUse = doorData.LockedMaterial;
                _doorRenderer.material = _materialToUse;
            }
            else
            {
                _materialToUse = doorData.UnlockedMaterial;
                _doorRenderer.material = _materialToUse;
            }
        }
        
        #endregion
        
        #region Event Functions

        private void OnWin()
        {
            _doorLocked = false;
            UpdateRendering();
        }

        #endregion
        
        #region Event Listeners
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.AllElectricBoxesCollided , OnWin);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.AllElectricBoxesCollided , OnWin);
        }
        
        #endregion
    }
}
