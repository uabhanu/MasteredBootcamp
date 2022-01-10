using UnityEngine;

namespace BhanuAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        #region Private Variables Declarations
        
        private bool startCutsceneWatched;
        private float _gravityValue = 9.81f;
        
        #endregion
        
        #region Serialized Field Private Variables Declarations
        
        [SerializeField] private float jumpForce;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int electricBoxesCollided = 0;
        [SerializeField] private int pipesInTheSocket = 0;
        [SerializeField] private Material localMaterial;
        [SerializeField] private Material remoteMaterial;

        #endregion
        
        #region Helper Functions
        
        public bool StartCutsceneWatched { get => startCutsceneWatched; set => startCutsceneWatched = value; }
        
        public float GravityValue
        {
            get => _gravityValue;
            set => _gravityValue = value;
        }
        
        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }
        
        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
        
        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }
        
        public int ElectricBoxesCollided
        {
            get => electricBoxesCollided;
            set => electricBoxesCollided = value;
        }
        
        public int PipesInTheSocket
        {
            get => pipesInTheSocket;
            set => pipesInTheSocket = value;
        }

        public Material LocalMaterial
        {
            get => localMaterial;
            set => localMaterial = value;
        }

        public Material RemoteMaterial
        {
            get => remoteMaterial;
            set => remoteMaterial = value;
        }

        #endregion
    }
}
