using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class FuelData : ScriptableObject
    {
        [SerializeField] private Vector3 fuelPosition;

        private void OnEnable()
        {
            fuelPosition = GameObject.Find("Fuel").transform.position;
        }

        public Vector3 FuelPosition
        {
            get => fuelPosition;
            set => fuelPosition = value;
        }
    }
}
