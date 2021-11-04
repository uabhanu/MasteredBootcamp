using UnityEngine;

namespace ScriptableOBjects
{
    [CreateAssetMenu]
    public class SpanwerData : ScriptableObject
    {
        [SerializeField] private GameObject gameObjectToSpawn;
        [SerializeField] private int maxNumberOfPatientsAllowedToSpawn;
        [SerializeField] private int numberOfPatientsPerSpawn;
        [SerializeField] private float repeatRate;

        public GameObject GameObjectToSpawn
        {
            get => gameObjectToSpawn;
            set => gameObjectToSpawn = value;
        }

        public int MaxNumberOfPatientsAllowedToSpawn
        {
            get => maxNumberOfPatientsAllowedToSpawn;
            set => maxNumberOfPatientsAllowedToSpawn = value;
        }
        
        public int NumberOfPatientsPerSpawn
        {
            get => numberOfPatientsPerSpawn;
            set => numberOfPatientsPerSpawn = value;
        }

        public float RepeatRate
        {
            get => repeatRate;
            set => repeatRate = value;
        }
    }
}
