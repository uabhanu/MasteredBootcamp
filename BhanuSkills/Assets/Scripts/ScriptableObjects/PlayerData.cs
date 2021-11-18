using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        // animation IDs
        private int _animIDFreeFall;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDMotionSpeed;
        private int _animIDSpeed;

        [SerializeField] private float fallTimeOut = 0.15f;
        [SerializeField] private float gravity = -15f;
        [SerializeField] private float jumpHeight = 1.2f;
        [SerializeField] private float jumpTimeOut = 0.5f;
        [SerializeField] private float rotationSmoothTime = 0.12f;
        [SerializeField] private float runSpeed = 5.4f;
        [SerializeField] private float speedChangeRate = 10f;
        [SerializeField] private float verticalVelocity;
        [SerializeField] private float walkSpeed = 2f;

        public int AnimIDFreeFall
        {
            get => _animIDFreeFall;
            set => _animIDFreeFall = value;
        }

        public int AnimIDGrounded
        {
            get => _animIDGrounded;
            set => _animIDGrounded = value;
        }

        public int AnimIDJump
        {
            get => _animIDJump;
            set => _animIDJump = value;
        }

        public int AnimIDMotionSpeed
        {
            get => _animIDMotionSpeed;
            set => _animIDMotionSpeed = value;
        }

        public int AnimIDSpeed
        {
            get => _animIDSpeed;
            set => _animIDSpeed = value;
        }
        
        public void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }
        
        public float FallTimeOut
        {
            get => fallTimeOut;
            set => fallTimeOut = value;
        }
        
        public float Gravity
        {
            get => gravity;
            set => gravity = value;
        }
        
        public float JumpHeight
        {
            get => jumpHeight;
            set => jumpHeight = value;
        }
        
        public float JumpTimeOut
        {
            get => jumpTimeOut;
            set => jumpTimeOut = value;
        }
        
        public float RotationSmoothTime
        {
            get => rotationSmoothTime;
            set => rotationSmoothTime = value;
        }
        
        public float RunSpeed
        {
            get => runSpeed;
            set => runSpeed = value;
        }

        public float SpeedChangeRate
        {
            get => speedChangeRate;
            set => speedChangeRate = value;
        }

        public float VerticalVelocity
        {
            get => verticalVelocity;
            set => verticalVelocity = value;
        }
        
        public float WalkSpeed
        {
            get => walkSpeed;
            set => walkSpeed = value;
        }
    }
}
