using System.Collections;
using UnityEngine;

namespace AI
{
    public class AIDetector : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private Transform _target = null; //Remove SerializedField after testing

        [SerializeField] private Color gizmosColour;
        [SerializeField] private float detectionCheckDelay = 0.1f;
        [Range(1 , 15)][SerializeField] private float viewRadius = 11;
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private LayerMask visibilityLayerMask;

        [field: SerializeField] public bool TargetVisible { get; private set; }  //Remove SerializedField after testing
        
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
                TargetVisible = CheckTargetVisible();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmosColour;
            Gizmos.DrawWireSphere(transform.position , viewRadius);
        }

        private bool CheckTargetVisible()
        {
            var result = Physics2D.Raycast(transform.position , Target.position - transform.position , viewRadius , visibilityLayerMask);

            if(result.collider != null)
            {
                return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
            }

            return false;
        }

        private IEnumerator DetectionCoroutine() //Recursive Function Warning
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
