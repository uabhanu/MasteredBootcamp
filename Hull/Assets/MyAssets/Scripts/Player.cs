using TMPro;
using UnityEngine;

namespace MyAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        private float _t;
        private int _playerID;
        private int _shipID;
        private TMP_Text _playerInfoTMP;
        private Transform _destinationTransform;
        private Vector3 _startPosition;

        [SerializeField] private Animator animator;
        [SerializeField] private float timeOfTravel;

        private void Start()
        {
            _destinationTransform = GameObject.FindWithTag("Destination").transform;
            _playerID = gameObject.GetInstanceID(); //This is just for testing purpose as this will normally be player ID from a multiplayer network.
            _playerInfoTMP = GameObject.Find("PlayerInfo (TMP)").GetComponent<TMP_Text>();
            _shipID = ShipID;
            _playerInfoTMP.text = "Player ID : " + _playerID + "\n" + "Ship ID : " + _shipID;
            _startPosition = transform.position;
        }

        private void Update()
        {
            _t += Time.deltaTime / timeOfTravel;
            transform.position = Vector3.Lerp(_startPosition , _destinationTransform.position , _t);

            if(transform.position.y < 5f)
            {
                animator.SetBool("ReadyToRotate" , true);
            }
        }
        
        public int ShipID
        {
            get => _shipID;
            set => _shipID = value;
        }
    }
}
