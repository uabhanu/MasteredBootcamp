using System.Collections;
using UnityEngine;

namespace AI
{
    public class AIDetector : MonoBehaviour
    {
        #region Variables
        
        private Transform _target = null;
        
        [SerializeField] private float detectionCheckDelay = 0.1f;
        [Range(1f , 15f)] [SerializeField] private float viewRadius = 11f;
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private LayerMask visibilityLayerMask;
        
        #endregion
        
        #region Functions

        private void Start()
        {
            StartCoroutine(DetectionCoroutine());
        }

        private void Update()
        {
            if(Target != null)
            {
                TargetVisible = CheckTargetVisibility();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , viewRadius);
        }

        private IEnumerator DetectionCoroutine() //This is just a recursive function warning
        {
            yield return new WaitForSeconds(detectionCheckDelay);
            DetectTarget();
            StartCoroutine(DetectionCoroutine());
        }
        
        private void CheckIfPlayerInRange()
        {
            Collider2D col2D = Physics2D.OverlapCircle(transform.position , viewRadius , playerLayerMask);

            if(col2D != null)
            {
                Target = col2D.transform;
            }
        }
        
        private bool CheckTargetVisibility()
        {
            // Interesting to know that unchecking Queries start in Colliders will detect the player 
            var visibleObj = Physics2D.Raycast(transform.position , Target.position - transform.position , viewRadius , visibilityLayerMask);

            if(visibleObj.collider != null)
            {
                // Compare binary values of layer mask using '&' binary operator
                return (playerLayerMask & (1 << visibleObj.collider.gameObject.layer)) != 0;
            }

            return false;
        }
        
        private void DetectIfOutOfRange()
        {
            if(Target == null || !Target.gameObject.activeSelf || Vector2.Distance(transform.position , Target.position) > viewRadius + 1)
            {
                Target = null;
            }
        }
        
        private void DetectTarget()
        {
            if(Target == null)
            {
                CheckIfPlayerInRange();
            }
            
            else if(Target != null)
            {
                DetectIfOutOfRange();
            }
        }

        [field: SerializeField]
        public bool TargetVisible { get; private set; }

        public Transform Target
        {
            get => _target;

            set
            {
                _target = value;
                TargetVisible = false;
            }
        }
        
        #endregion
    }
}
