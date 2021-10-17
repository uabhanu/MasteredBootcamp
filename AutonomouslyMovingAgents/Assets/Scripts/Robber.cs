using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

//TODO Use Finite State Machines to implement the Robber Behaviour if time permits
public class Robber : MonoBehaviour
{
    [SerializeField] private CopData copData;
    [SerializeField] private NavMeshAgent agent;
    
    public GameObject target;
    
    private void Update()
    {
        //Flee(target.transform.position);
        Pursue();
    }
    
    private void Flee(Vector3 location)
    {
        var fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector); 
    }

    private void Pursue()
    {
        var targetDir = target.transform.position - transform.position;
        var lookAhead = targetDir.magnitude / (agent.speed + copData.CurrentSpeed);
        var relativeHeading = Vector3.Angle(transform.forward , transform.TransformVector(target.transform.forward));
        var toTarget = Vector3.Angle(transform.forward , transform.TransformVector(targetDir));

        if((toTarget > 90 && relativeHeading < 20) || (copData.CurrentSpeed < 0.01f))
        {
            Seek(target.transform.position);
        }
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
