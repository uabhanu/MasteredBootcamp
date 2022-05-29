using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        private float _t;
        private Vector3 _startPosition;

        [SerializeField] private Animator animator;
        [SerializeField] private float timeOfTravel;
        [SerializeField] private Transform targetTransform;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            _t += Time.deltaTime / timeOfTravel;
            transform.position = Vector3.Lerp(_startPosition , targetTransform.position , _t);

            if(transform.position.y < 5f)
            {
                animator.SetBool("ReadyToRotate" , true);
            }
        }
    }
}
