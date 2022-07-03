using System.Collections;
using UnityEngine;

namespace Characters.AI
{
    public class AIDetector : MonoBehaviour
    {
        #region Private Variable Declerations
        
        private bool _targetVisible;
        [SerializeField] private Transform _visibleTargetTransform;
        
        #endregion
        
        #region Serialized Field Private Variable Declerations
        
        [SerializeField] private float detectionCheckDelay = 0.1f;
        [Range(1f , 15f)][SerializeField] private float viewRadius = 11f;
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private LayerMask visibilityLayerMask;
        
        #endregion
        
        #region Enumerator Functions

        private IEnumerator DetectionCoroutine() //This is Recursive Function Warning
        {
            yield return new WaitForSeconds(detectionCheckDelay);
            DetectTarget();
            StartCoroutine(DetectionCoroutine());
        }
        
        #endregion
        
        #region MonoBehaviour Functions

        private void Start()
        {
            _visibleTargetTransform = null;
            StartCoroutine(DetectionCoroutine());
        }

        private void Update()
        {
            if(VisibleTargetTransform != null)
            {
                _targetVisible = TargetVisibleOrNot();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , viewRadius);
        }

        #endregion

        #region Getters & Setters
        
        public bool TargetVisible
        {
            get => _targetVisible;
            set => _targetVisible = value;
        }
        
        public Transform VisibleTargetTransform
        {
            get => _visibleTargetTransform;
            set
            {
                _visibleTargetTransform = value;
                TargetVisible = false;
            }
        }
        
        #endregion
        
        #region Custom Functions

        // The warning is for transform
        private bool TargetVisibleOrNot()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position ,VisibleTargetTransform.position - transform.position , viewRadius , visibilityLayerMask);

            if(hit2D.collider != null)
            {
                return (playerLayerMask & (1 << hit2D.collider.gameObject.layer)) != 0;
            }

            return false;
        }
        
        private void CheckIfPlayerInRange()
        {
            Collider2D col2D = Physics2D.OverlapCircle(transform.position , viewRadius , playerLayerMask);

            if(col2D != null)
            {
                VisibleTargetTransform = col2D.transform;
            }
        }
        
        private void DetectIfOutOfRange()
        {
            float distanceFromTarget = Vector2.Distance(transform.position , VisibleTargetTransform.position);
            
            //Not sure if this Target is null check is necessary
            if(VisibleTargetTransform == null || !VisibleTargetTransform.gameObject.activeSelf || distanceFromTarget > viewRadius)
            {
                VisibleTargetTransform = null;
            }
        }
        
        private void DetectTarget()
        {
            if(VisibleTargetTransform == null)
            {
                CheckIfPlayerInRange();
            }
            
            else if(VisibleTargetTransform != null)
            {
                DetectIfOutOfRange();
            }
        }

        #endregion
    }
}
