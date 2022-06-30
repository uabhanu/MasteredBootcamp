using UnityEngine;

namespace Characters
{
    public class Tank : MonoBehaviour
    {
        #region Private Variable Declarations
    
        private Vector2 _movementVector;
    
        #endregion
    
        #region Private Serialized Field Variable Declarations

        [SerializeField] private TankBodyMover tankBodyMover;

        #endregion

        #region MonoBehaviour Functions
    
        private void Awake()
        {
            if(tankBodyMover == null) // If this is not assigned in the inspector
            {
                tankBodyMover = GetComponentInChildren<TankBodyMover>();
            }
        }
    
        #endregion

        #region Unity Event Functions
    
        public void HandleMoveBody(Vector2 movementVector)
        {
            tankBodyMover.MoveBody(movementVector);
        }

        #endregion
    }
}
