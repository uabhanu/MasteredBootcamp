using Random = UnityEngine.Random;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject _sphereObj;
    private Vector2 _randomPointOnCircle;
    
    [SerializeField] private int radius;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero , radius);
    }

    public void RandomizePosition()
    {
        _randomPointOnCircle = Random.insideUnitCircle * radius;
        
        if(_sphereObj != null)
        {
            _sphereObj.transform.position = _randomPointOnCircle;   
        }
    }

    public void SpawnSphere()
    {
        _sphereObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }
}
