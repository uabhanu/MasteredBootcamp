using ScriptableOBjects;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpanwerData spawnerData;

    private void Start()
    {
        InvokeRepeating("SpawnPatient" , 0f , spawnerData.RepeatRate);
    }

    private void SpawnPatient()
    {
        Instantiate(spawnerData.GameObjectToSpawn , transform.position , Quaternion.identity);
    }
}
