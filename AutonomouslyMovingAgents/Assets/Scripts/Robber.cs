using UnityEngine;
using UnityEngine.AI;

public class Robber : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    
    public GameObject target;
    
    private void Update()
    {
        Flee(target.transform.position);
    }
    
    private void Flee(Vector3 location)
    {
        var fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector); 
    }
    
    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }
    
    //Bhanu created this which can be used if we require the Robber to chase the Cop 
    private void Towards(Vector3 location)
    {
        var towardsVector = location - transform.position;
        agent.SetDestination(transform.position + towardsVector); 
    }
}
